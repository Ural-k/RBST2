using UnityEngine;
using DG.Tweening;

/// <summary>
/// –òŽt
/// </summary>
public class Pharmacist : TestPlayerBase
{
    int amaunt_;
    Potion potion_;
    enum Potion
    {
        Water,
        Power,
        Heal,
        Poison
    }

    protected override void Skill1()
    {
        var demo = TestSkill.Instance.GetHitEnemy(Vector2.zero, 3);
        switch (potion_)
        {
            case Potion.Water:
                break;
            case Potion.Power:
                break;
            case Potion.Heal:
                break;
            case Potion.Poison:
                break;
        }
    }
    protected override void Skill2()
    {

    }
    protected override void Skill3()
    {
        
    }
}
