using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/EffectData")]
public class EffectData : ScriptableObject
{
    [Header("ID / 表示")]
    public int id_;
    public string name_;
    public Sprite icon_;
    public Sprite arrow_;

    [Header("初期効果値 / スタックによる上昇値 / 最大スタック数")]
    public int initalPower_;
    public int stackPower_;
    public int maxStack_ = 1;

    [Header("効果")]
    public EffectType type_;
    [SerializeReference, SubclassSelector] public List<IEffect> effects_ = new();
}
