using UnityEngine;

public interface IInputSkill
{
    void Skill1();
    void Skill2();
    void Skill3();
}

public class Job : MonoBehaviour, IInputSkill
{
    [SerializeField] private string jobName_;

    [SerializeField] private SkillData skill1_;
    [SerializeField] private SkillData skill2_;
    [SerializeField] private SkillData skill3_;

    private ITargetCircle me_;
    private float gcd_;

    private void Start()
    {
        me_ = GetComponent<Player>().GetComponent<ITargetCircle>();
    }

    void IInputSkill.Skill1()
    {
        foreach(var a in skill1_.attacks_)
        {
            a.Execute(me_);
        }
    }

    void IInputSkill.Skill2()
    {
    }

    void IInputSkill.Skill3()
    {
    }
}
