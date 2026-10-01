using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("ï\é¶")]
    public string skillName_;
    public Sprite icon_;
    [TextArea(3, 10), Multiline(5)]
    public string help_;

    [Header("ã@î\")]
    public float gcd_;
    public float cd_;
    public SkillData nextSkill_;

    [Header("çUåÇ")]
    public List<AttackBase> attacks_;
}
