using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// β版以降に使用したいプレイヤーベース
/// </summary>
public abstract class TestPlayerBase : MonoBehaviour,ITestTargetCircle
{
    /* バフ・デバフ */
    private readonly List<Effect> activeEffect_ = new();
    private float tickTimer_;

    public event Action<Effect> OnBuffApplied;
    public event Action<Effect> OnBuffRemoved;

    void ITestTargetCircle.AddEffect(EffectData data, int time)
    {
        Debug.Log("AddEffect");
        var effect = activeEffect_.Find(n => n.data_.id_ == data.id_);
        if (effect != null && data.maxStack_ > 1)
        {
            effect.stackCount_ = Mathf.Min(effect.stackCount_ + 1, data.maxStack_);
            effect.timer_ = time;
            return;
        }

        var instance = new Effect { data_ = data, timer_ = time, target_ = GetComponent<ITestTargetCircle>() };
        activeEffect_.Add(instance);
        foreach (var e in data.effects_) e.OnApply(instance);
        OnBuffApplied?.Invoke(instance);
    }

    void ITestTargetCircle.RemoveEffect(EffectData data)
    {
        var effect = activeEffect_.Find(n => n.data_.id_ == data.id_);
        if (effect == null) { Debug.Log("なし"); return; }

        foreach (var e in data.effects_) e.OnRemove(effect);
        OnBuffRemoved?.Invoke(effect);
        activeEffect_.Remove(effect);
    }

    void ITestTargetCircle.RemoveEffect(int num)
    {
        for (int i = 0; i < num; ++i)
        {
            var effect = activeEffect_.OrderByDescending(n => n.timer_).First();//秒数の多い方から解除
            foreach (var e in effect.data_.effects_) e.OnRemove(effect);
            OnBuffRemoved?.Invoke(effect);
            activeEffect_.Remove(effect);
        }
    }

    public void TickEffect()
    {
        tickTimer_ += Time.deltaTime;
        if (tickTimer_ < 1f) return;
        tickTimer_ -= 1f;

        for (int i = activeEffect_.Count - 1; i >= 0; --i)
        {
            var buff = activeEffect_[i];
            buff.timer_ -= 1f;
            foreach (var effect in buff.data_.effects_) effect.OnTick(buff);

            if (buff.timer_ <= 0)
            {
                foreach (var effect in buff.data_.effects_) effect.OnRemove(buff);
                OnBuffRemoved?.Invoke(buff);
                activeEffect_.RemoveAt(i);
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

    private void Awake() => PlayerManager.AddPlayer(new Player());

    private void Update()
    {
        transform.position =
        new Vector2(
            Mathf.Clamp(transform.position.x + moveAxis_.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
            Mathf.Clamp(transform.position.y + moveAxis_.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
        );

        TickEffect();
    }
}