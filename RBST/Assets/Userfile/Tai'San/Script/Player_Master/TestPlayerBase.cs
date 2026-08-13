using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// マスター版に使用したいプレイヤーベース(ローカル)
/// </summary>
public abstract class Player : MonoBehaviour,ITestTargetCircle
{
    /* 移動範囲 */
    protected const float MOVE_SCREEN_X = 8.8f;
    protected const float MOVE_SCREEN_Y = 4.8f;

    [SerializeField] protected TestParameter parameter_;
    [SerializeField] protected Animator animator_;

    protected int IS_MOVING_HASH = Animator.StringToHash("IsMoving");
    protected int[] nowCombo_ = new int[2];
    protected float gcd_;
    protected float[] cd_ = new float[3];
    private Vector2 moveAxis_;
    private Vector2 lastAxis_;

    public TestParameter Parameter { get { return parameter_; } set { parameter_ = value; } }
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
        ParticleManager.InstanceLoad();//超仮(範囲表示)
    }

    /* 入力 */
    public void InputSkill1(InputValue value) { if (value.isPressed) Skill1(); }
    public void InputSkill2(InputValue value) { if (value.isPressed) Skill2(); }
    public void InputSkill3(InputValue value) { if (value.isPressed) Skill3(); }
    public void Move(InputValue value) => moveAxis_ = value.Get<Vector2>();

    private void Update()
    {
        gcd_ = Mathf.Max(gcd_ - Time.deltaTime, 0);
        for (int i = 0; i < cd_.Count(); ++i) cd_[i] = Mathf.Max(cd_[i] - Time.deltaTime, 0);
        if(moveAxis_ != Vector2.zero)
        {
            var move = parameter_.spd_ * Time.deltaTime * moveAxis_;
            lastAxis_ = moveAxis_;
            transform.position =
            new Vector2(
                Mathf.Clamp(transform.position.x + move.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
                Mathf.Clamp(transform.position.y + move.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
            );
        }

        Effect.TickEffect();
    }

    protected abstract void Skill1(int s = 0);
    protected abstract void Skill2(int s = 1);
    protected abstract void Skill3(int s = 2);
    protected float Distance(int d) { return Mathf.Sign(lastAxis_.x) * d; }
    protected bool IsGCD() { return gcd_ > 0; }
    protected bool IsCD(int s) { return cd_[s] > 0; }
    protected bool IsGCDCD(int s) { return gcd_ > 0 || cd_[s] > 0; }
    protected void ComboBreak(int s) { for (int i = 0; i < nowCombo_.Count(); ++i) if (i != s) nowCombo_[i] = 0; }

    Vector2 ITestTargetCircle.GetPosition => transform.position;
    void ITestTargetCircle.TakeDamage(int point) { parameter_.hp_ -= point; }
    void ITestTargetCircle.TakeHeal(int point) { parameter_.hp_ += point; }
}