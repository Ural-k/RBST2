using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// マスター版に使用したいプレイヤーベース(ローカル)
/// </summary>
public class Player : MonoBehaviour,ITargetCircle
{
    /* 移動範囲(仮かも) */
    protected const float MOVE_SCREEN_X = 8.8f;
    protected const float MOVE_SCREEN_Y = 4.8f;

    [SerializeField] private Parameter parametor_;
    [SerializeField] protected Animator animator_;

    private bool[] hold_ = new bool[3];
    private Vector2 moveAxis_;
    private Vector2 lastAxis_;

    protected int IS_MOVING_HASH = Animator.StringToHash("IsMoving");
    protected int[] nowCombo_ = new int[2];
    protected float gcd_;
    protected float[] cd_ = new float[3];

    //ターゲットサークル
    public Parameter Parameter => parametor_;
    public Effect Effect { get; set; }
    public float Radius { get; set; } = 0.1f;
    //多分仮２つ(UIでしか使わない可能性がある)↓
    public float GetGCD { get { return gcd_; } }
    public float[] GetCD { get { return cd_; } }

    private void Start()
    {
        Effect = new(this);
        InputManager.Instance.OnMove_ += Move;
        InputManager.Instance.OnSkill1_ += InputSkill1;
        InputManager.Instance.OnSkill2_ += InputSkill2;
        InputManager.Instance.OnSkill3_ += InputSkill3;
        PlayerManager.AddPlayer(this);
    }

    private void Update()
    {
        gcd_ = Mathf.Max(gcd_ - Time.deltaTime, 0);
        for (int i = 0; i < cd_.Count(); ++i) cd_[i] = Mathf.Max(cd_[i] - Time.deltaTime, 0);
        if(moveAxis_ != Vector2.zero)
        {
            var move = (parametor_.spd_ * 0.05f) * Time.deltaTime * moveAxis_;
            lastAxis_ = moveAxis_;
            transform.position =
            new Vector2(
                Mathf.Clamp(transform.position.x + move.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
                Mathf.Clamp(transform.position.y + move.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
            );
        }

        Effect.TickEffect();
    }

    /* 入力 */
    public void InputSkill1(InputValue value) { }
    public void InputSkill2(InputValue value) { }
    public void InputSkill3(InputValue value) { }
    public void Move(InputValue value) => moveAxis_ = value.Get<Vector2>();

    protected float Distance(int d) { return Mathf.Sign(lastAxis_.x) * d; }
    protected bool IsGCD() { return gcd_ > 0; }
    protected bool IsCD(int s) { return cd_[s] > 0; }
    protected bool IsGCDCD(int s) { return gcd_ > 0 || cd_[s] > 0; }
    protected void ComboBreak(int s) { for (int i = 0; i < nowCombo_.Count(); ++i) if (i != s) nowCombo_[i] = 0; }

    //ターゲットサークル
    Vector2 ITargetCircle.GetPosition => transform.position;
    void ITargetCircle.TakeDamage(int point,ITargetCircle from)
    {
        ShowFloatingText(point, FloatingTextType.PlayerDamage);
        parametor_.hp_ -= point;
    }
    void ITargetCircle.TakeHeal(int point, ITargetCircle from)
    {
        ShowFloatingText(point, FloatingTextType.Heal);
        parametor_.hp_ += point;
    }

    /// <summary>
    /// ダメージテキストの呼び出し
    /// </summary>
    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }

}