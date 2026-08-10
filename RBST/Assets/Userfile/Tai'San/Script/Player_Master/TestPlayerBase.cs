using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// β版以降に使用したいプレイヤーベース
/// </summary>
public abstract class TestPlayerBase : MonoBehaviour,ITestTargetCircle
{
    /* バフ・デバフ */
    public Effect Effect { get; set; }

    /* スクリーン制限範囲 */
    protected const float MOVE_SCREEN_X = 8.8f;
    protected const float MOVE_SCREEN_Y = 4.8f;
    private const float TARGET_RADIUS = 0.1f;

    /* アニメーション */
    [SerializeField]
    protected Animator animator_;

    /* スキル */
    private List<List<float>> cd_;
    private int nowCombo_;
    private float gcd_;

    /* ステータス */
    public float CircleRadius { get; set; } = 0.1f;
    [SerializeField]
    protected TestParameter parameter_;
    private Vector2 moveAxis_;

    protected int IS_MOVING_HASH = Animator.StringToHash("IsMoving");

    /* プロパティ */
    public int HP { get { return parameter_.hp_; } set { parameter_.hp_ = value; } }

    private void Start()
    {
        Effect = new Effect(this);
        InputManager.Instance.onMove_ += Move;
        PlayerManager.AddPlayer(new Player());
    }

    private void Update()
    {
        transform.position =
        new Vector2(
            Mathf.Clamp(transform.position.x + moveAxis_.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
            Mathf.Clamp(transform.position.y + moveAxis_.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
        );

        Effect.TickEffect();
    }

    protected abstract void Skill1();
    protected abstract void Skill2();
    protected abstract void Skill3();

    /* 入力 */
    public void InputSkill1(InputAction.CallbackContext context) { if (context.performed) Skill1(); }
    public void InputSkill2(InputAction.CallbackContext context) { if (context.performed) Skill2(); }
    public void InputSkill3(InputAction.CallbackContext context) { if (context.performed) Skill3(); }

    void ITestTargetCircle.TakeDamage(float point) { HP -= (int)point; Debug.Log($"TakeDamage : {point}"); }
    void ITestTargetCircle.TakeHeal(float point) { HP += (int)point; }

    /// <summary>
    /// 移動入力
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        moveAxis_ = context.ReadValue<Vector2>();
        moveAxis_ *= parameter_.spd_ * Time.deltaTime;
    }
}