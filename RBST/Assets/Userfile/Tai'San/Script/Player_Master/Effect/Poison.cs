using System;

[Serializable]
public class AttackDeltaTime : IEffect
{
    public float damagePerTick_ = 5f;
    public void OnApply(Effect i) { }
    public void OnTick(Effect i) => i.target_.TakeDamage(damagePerTick_ * i.stackCount_);
    public void OnRemove(Effect i) { }
}


[Serializable]
public class AttackUpEffect : IEffect
{
    public float multiplier_ = 1.2f;
    public void OnApply(Effect i) => i.target_.TakeDamage(5 * i.stackCount_);
    public void OnTick(Effect i) { }
    public void OnRemove(Effect i) => i.target_.TakeDamage(5 * i.stackCount_);
}