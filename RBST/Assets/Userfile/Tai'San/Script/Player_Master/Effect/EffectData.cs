using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/Player/EffectData")]
public class EffectData : ScriptableObject
{
    public string buffId;
    public string displayName;
    public Sprite icon;
    public float duration = 5f;
    public bool isStackable;
    public int maxStack = 1;

    // ScriptableObjectのサブアセットとして効果を差し込む
    [SerializeReference] public List<IEffect> effects = new();
}
