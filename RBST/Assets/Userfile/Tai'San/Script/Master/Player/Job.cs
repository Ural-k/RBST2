using UnityEngine;

public interface InputSkill
{
    void Skill1();
    void Skill2();
    void Skill3();
}

public class Job : MonoBehaviour, InputSkill
{
    [SerializeField] private string jobName_;

    [SerializeField] private SkillData skill1_;
    [SerializeField] private SkillData skill2_;
    [SerializeField] private SkillData skill3_;

    private float gcd_;

    void InputSkill.Skill1()
    {
        
    }

    void InputSkill.Skill2()
    {
    }

    void InputSkill.Skill3()
    {
    }
}
