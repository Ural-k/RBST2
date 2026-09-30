using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField] private string jobName_;
    [SerializeField] protected Animator animator_;
    [SerializeField] private Parameter parametor_;

    [SerializeField] private SkillData skill1_;
    [SerializeField] private SkillData skill2_;
    [SerializeField] private SkillData skill3_;

    private ITargetCircle myTargetCircle_;
    private Vector2 moveAxis_;
    private Vector2 lastAxis_;

    protected int IS_MOVING_HASH = Animator.StringToHash("IsMoving");
    protected int[] nowCombo_ = new int[2];
    protected float gcd_;
    protected float[] cd_ = new float[3];

    public SkillData GetSkill1 { get { return skill1_; } }
    public SkillData GetSkill2 { get { return skill2_; } }
    public SkillData GetSkill3 { get { return skill3_; } }

    //プロパティ
    public float GetGCD { get { return gcd_; } }
    public float[] GetCD { get { return cd_; } }
    //ターゲットサークル
    public Parameter Parameter => parametor_;
    public Effect Effect { get; set; }
    public float Radius { get; set; } = 0.1f;

    private void Start()
    {
        Effect = new(this);
        myTargetCircle_ = GetComponent<ITargetCircle>();
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
    public void InputSkill1(InputValue value)
    {
        if (gcd_ > 0 || cd_[0] > 0) return;
        gcd_ = skill1_.gcd_;
        cd_[0] = skill1_.cd_;
        foreach (var a in skill1_.attacks_) { a.Execute(myTargetCircle_); }
        skill1_ = skill1_.nextSkill_;//次のスキルへ
    }
    public void InputSkill2(InputValue value)
    {
        if (gcd_ > 0 || cd_[1] > 0) return;
        gcd_ = skill2_.gcd_;
        cd_[1] = skill2_.cd_;
        foreach (var a in skill2_.attacks_) { a.Execute(myTargetCircle_); }
        skill1_ = skill2_.nextSkill_;
    }
    public void InputSkill3(InputValue value)
    {
        if (gcd_ > 0 || cd_[2] > 0) return;
        gcd_ = skill3_.gcd_;
        cd_[2] = skill3_.cd_;
        foreach (var a in skill3_.attacks_) { a.Execute(myTargetCircle_); }
        skill1_ = skill3_.nextSkill_;
    }
    public void Move(InputValue value) => moveAxis_ = value.Get<Vector2>();

    protected float Distance(int d) { return Mathf.Sign(lastAxis_.x) * d; }
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