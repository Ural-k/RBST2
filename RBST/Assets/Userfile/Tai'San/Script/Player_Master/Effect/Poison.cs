using System;

[Serializable]
public class DamageDeltaTime : IEffect
{
    public float damagePerTick_ = 5f;
    public void OnApply(EffectController i) { }
    public void OnTick(EffectController i) => i.target_.TakeDamage(50);
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