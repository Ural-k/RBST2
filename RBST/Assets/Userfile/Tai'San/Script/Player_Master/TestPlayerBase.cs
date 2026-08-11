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
    protected float[] cd_;
    private Vector2 moveAxis_;

    public TestParameter Parameter { get { return parameter_; } set { parameter_ = value; } }
    public Effect Effect { get; set; }
    public float Radius { get; set; } = 0.1f;

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
    public void InputSkill1(InputValue value) { if (value.isPressed && gcd_ <= 0 && cd_[0] <= 0) Skill1(); }
    public void InputSkill2(InputValue value) { if (value.isPressed && gcd_ <= 0 && cd_[1] <= 0) Skill2(); }
    public void InputSkill3(InputValue value) { if (value.isPressed && gcd_ <= 0 && cd_[2] <= 0) Skill3(); }
    public void Move(InputValue value)
    {
        moveAxis_ = value.Get<Vector2>();
        moveAxis_ *= parameter_.spd_ * Time.deltaTime;
    }

    private void Update()
    {
        gcd_ = Mathf.Max(gcd_ - Time.deltaTime, 0);
        for (int i = 0; i < cd_.Count(); ++i) 
            cd_[i] = Mathf.Max(cd_[i] - Time.deltaTime, 0);
        transform.position =
        new Vector2(
            Mathf.Clamp(transform.position.x + moveAxis_.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
            Mathf.Clamp(transform.position.y + moveAxis_.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
        );

        Effect.TickEffect();
    }

    protected abstract void Skill1(int s = 0);
    protected abstract void Skill2(int s = 1);
    protected abstract void Skill3(int s = 2);

    Vector2 ITestTargetCircle.GetPosition => transform.position;
    void ITestTargetCircle.TakeDamage(int point) { parameter_.hp_ -= point; }
    void ITestTargetCircle.TakeHeal(int point) { parameter_.hp_ += point; }
}