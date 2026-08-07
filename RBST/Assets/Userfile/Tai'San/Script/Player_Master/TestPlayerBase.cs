using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// β版以降に使用したいプレイヤーベース
/// </summary>
public abstract class TestPlayerBase : MonoBehaviour,ITestTargetCircle
{
    /* バフ・デバフ */
    private readonly List<Effect> activeBuffs_ = new();
    private float tickTimer_;

    public event Action<Effect> OnBuffApplied;
    public event Action<Effect> OnBuffRemoved;

    void ITestTargetCircle.AddEffect(EffectData data)
    {
        var existing = activeBuffs_.Find(b => b.data_.id_ == data.id_);
        if (existing != null && data.maxStack_ > 1)
        {
            existing.stackCount_ = Mathf.Min(existing.stackCount_ + 1, data.maxStack_);
            existing.remainingTime_ = data.tickTime_; // リフレッシュ
            return;
        }

        var instance = new Effect { data_ = data, remainingTime_ = data.tickTime_, target_ = GetComponent<TestPlayerBase>() };
        activeBuffs_.Add(instance);
        foreach (var effect in data.effects_) effect.OnApply(instance);
        OnBuffApplied?.Invoke(instance);
    }

    public void TickEffect()
    {
        tickTimer_ += Time.deltaTime;
        if (tickTimer_ < 1f) return;
        tickTimer_ -= 1f;

        for (int i = activeBuffs_.Count - 1; i >= 0; --i)
        {
            var buff = activeBuffs_[i];
            buff.remainingTime_ -= 1f;
            foreach (var effect in buff.data_.effects_) effect.OnTick(buff);

            if (buff.IsExpired)
            {
                foreach (var effect in buff.data_.effects_) effect.OnRemove(buff);
                OnBuffRemoved?.Invoke(buff);
                activeBuffs_.RemoveAt(i);
            }
        }
    }

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
    [SerializeField]
    protected TestParameter parameter_;
    private Vector2 moveAxis_;

    protected int IS_MOVING_HASH = Animator.StringToHash("IsMoving");

    /* プロパティ */
    public int HP { get { return parameter_.hp_; } set { parameter_.hp_ = value; } }

    protected abstract void Skill1();
    protected abstract void Skill2();
    protected abstract void Skill3();

    /* 入力 */
    public void InputSkill1(InputAction.CallbackContext context) { if (context.performed) Skill1(); }
    public void InputSkill2(InputAction.CallbackContext context) { if (context.performed) Skill2(); }
    public void InputSkill3(InputAction.CallbackContext context) { if (context.performed) Skill3(); }

    float ITestTargetCircle.GetTargetRadius() { return TARGET_RADIUS; }
    void ITestTargetCircle.TakeDamage(float point) { HP -= (int)point; }
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

    private void Awake() => PlayerManager.AddPlayer(new Player());

    private void Update() => transform.position =
        new Vector2(
            Mathf.Clamp(transform.position.x + moveAxis_.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
            Mathf.Clamp(transform.position.y + moveAxis_.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
        );
}

[System.Serializable]
public struct TestParameter
{
    public int hp_;
    public int maxHp_;
    public int atk_;
    public int def_;
    public int spd_;
}