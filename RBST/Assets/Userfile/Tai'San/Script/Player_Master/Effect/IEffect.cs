
using System;

public interface IEffect
{
    /// <summary>
    /// 付与時の呼び出し
    /// </summary>
    void OnApply(EffectController instance);
    /// <summary>
    /// 毎秒の呼び出し
    /// </summary>
    /// <param name="instance"></param>
    void OnTick(EffectController instance);
    /// <summary>
    /// 解除時の呼び出し
    /// </summary>
    void OnRemove(EffectController instance);
}

[Serializable]
public class DamageDeltaTime : IEffect
{
    public float damagePerTick_ = 5f;
    public void OnApply(EffectController i) { }
    public void OnTick(EffectController i) => i.target_.TakeDamage(i.data_.initalPower_ + (i.stackCount_ - 1) * i.data_.stackPower_);
    public void OnRemove(EffectController i) { }
}

[Serializable]
public class AttackUpEffect : IEffect
{
    public float multiplier_ = 1.2f;
    public void OnApply(EffectController i) => i.target_.TakeDamage(5 * i.stackCount_);
    public void OnTick(EffectController i) { }
    public void OnRemove(EffectController i) => i.target_.TakeDamage(5 * i.stackCount_);
}
