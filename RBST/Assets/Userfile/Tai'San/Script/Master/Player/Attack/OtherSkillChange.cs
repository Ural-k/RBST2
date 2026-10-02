using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/OtherSkillChange")]
public class OtherSkillChange : AttackBase
{
    [System.Serializable]
    public struct ChangeSkill
    {
        public int skillNumber_;
        public SkillData changeSkill_;
    }
    [SerializeField] private List<ChangeSkill> changeSkills_ = new List<ChangeSkill>();

    public override void Execute(ITargetCircle from, Player player)
    {
        foreach(var cs in changeSkills_)
        {
            switch (cs.skillNumber_)
            {
                case 1:
                    player.GetSkill1 = cs.changeSkill_;
                    break;
                case 2:
                    player.GetSkill2 = cs.changeSkill_;
                    break;
                case 3:
                    player.GetSkill3 = cs.changeSkill_;
                    break;
            }
        }
    }
}
