using System.Collections.Generic;
using UnityEngine;


public class Skill : ScriptableObject
{
    [SerializeField] private List<SkillData> data_;
}

public struct SkillData
{
    public int power_;
    public float delay_;
    public Sprite icon_;

}