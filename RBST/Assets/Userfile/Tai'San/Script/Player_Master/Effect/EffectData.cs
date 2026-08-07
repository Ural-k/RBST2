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

    [Header("発動間隔")]
    public float tickTime_ = 5f;

    [Header("効果内容")]
    [SerializeReference, SubclassSelector] public List<IEffect> effects_ = new();
}
