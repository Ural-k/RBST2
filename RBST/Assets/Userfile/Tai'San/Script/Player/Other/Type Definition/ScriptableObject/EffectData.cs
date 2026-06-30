using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/Player/EffectData")]
public class EffectData : ScriptableObject
{
    [SerializeField] private List<SetBuff> buff_;
    [SerializeField] private List<SetDebuff> debuff_;


    [System.Serializable]
    struct SetBuff
    {
        public Buff buff_;
        public EffectInfo effectInfo_;
    }
    [System.Serializable]
    struct SetDebuff
    {
        public Debuff debuff_;
        public EffectInfo effectInfo_;
    }

}

public enum Buff
{
    None = 0,
    Buff1 = 1 << 0,
    Buff2 = 1 << 1,
    Buff3 = 1 << 2,
    Buff4 = 1 << 3,
    Buff5 = 1 << 4,
    Buff6 = 1 << 5,
}

public enum Debuff
{
    None = 0,
    Touen = 1 << 0,
    Count,
}

[System.Serializable]
public struct EffectInfo
{
    public TEffect effect_;
    Parameter parameter_;
    public float time_;
    public float deltaTime;
    public float power_;
}

/*
 :  効果テンプレート
 */
[System.Serializable]
public abstract record TEffect
{
    public static implicit operator TEffect(Buff buff) => new BuffEffect(buff);
    public static implicit operator TEffect(Debuff debuff) => new DebuffEffect(debuff);
}
public record BuffEffect(Buff Value) : TEffect;
public record DebuffEffect(Debuff Value) : TEffect;