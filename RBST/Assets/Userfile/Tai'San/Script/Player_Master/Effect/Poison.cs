using System;

[Serializable]
public class Poison : IEffect
{
    public float damagePerTick = 5f;
    public void OnApply(Effect i) { }
    public void OnTick(Effect i) => i.Target.TakeDamage(damagePerTick * i.StackCount);
    public void OnRemove(Effect i) { }
}


[Serializable]
public class AttackUpEffect : IEffect
{
    public float multiplier = 1.2f;
    public void OnApply(Effect i) => i.Target.TakeDamage(5 * i.StackCount);
    public void OnTick(Effect i) { }
    public void OnRemove(Effect i) => i.Target.TakeDamage(5 * i.StackCount);
}