using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/Player/EffectData")]
public class EffectData : ScriptableObject
{
    [SerializeField] private EffectInfo[] effectInfo_;
    public EffectInfo[] GetEffectInfo { get { return effectInfo_; } }
}

public enum Buff
{
    None,
    Attack,
    Sprint,

}

public enum Debuff
{
    Touen,
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
public abstract record TEffect;
public record BuffEffect(Buff Value) : TEffect;
public record DebuffEffect(Debuff Value) : TEffect;