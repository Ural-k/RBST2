using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("ï\é¶")]
    public string skillName_;
    public Sprite icon_;
    public string help_;

    [Header("ã@î\")]
    public int power_;
    public float gcd_;
    public float cd_;
    public float delay_;
    public SkillData nextSkill_;

    [Header("çUåÇ")]
    public List<PlayerAttackBase> attacks_;
}
