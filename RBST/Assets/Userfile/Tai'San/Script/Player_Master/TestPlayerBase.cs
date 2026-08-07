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
    private readonly List<Effect> _activeBuffs = new();
    private float _tickTimer;

    public event Action<Effect> OnBuffApplied;
    public event Action<Effect> OnBuffRemoved;

    public void AddEffect(EffectData data)
    {
        var existing = _activeBuffs.Find(b => b.Data.buffId == data.buffId);
        if (existing != null && data.isStackable)
        {
            existing.StackCount = Mathf.Min(existing.StackCount + 1, data.maxStack);
            existing.RemainingTime = data.duration; // リフレッシュ
            return;
        }

        var instance = new Effect { Data = data, RemainingTime = data.duration, Target = GetComponent<TestPlayerBase>() };
        _activeBuffs.Add(instance);
        foreach (var effect in data.effects) effect.OnApply(instance);
        OnBuffApplied?.Invoke(instance);
    }

    public void TakeDamage(float f) { }

    public void TickEffect()
    {
        _tickTimer += Time.deltaTime;
        if (_tickTimer < 1f) return;
        _tickTimer -= 1f;

        for (int i = _activeBuffs.Count - 1; i >= 0; --i)
        {
            var buff = _activeBuffs[i];
            buff.RemainingTime -= 1f;
            foreach (var effect in buff.Data.effects) effect.OnTick(buff);

            if (buff.IsExpired)
            {
                foreach (var effect in buff.Data.effects) effect.OnRemove(buff);
                OnBuffRemoved?.Invoke(buff);
                _activeBuffs.RemoveAt(i);
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
    void ITestTargetCircle.SabHitPoint(int point) { HP -= point; }

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