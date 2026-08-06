using UnityEngine;
using DG.Tweening;

/// <summary>
/// ñÚét
/// </summary>
public class Pharmacist : TestPlayerBase
{
    int amaunt_;//écó ?
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
            case Potion.Power:
                break;
            case Potion.Heal:
                break;
            case Potion.Poison:
                break;
            default: break;//êÖ
        }
    }
    protected override void Skill2()
    {

    }
    protected override void Skill3()
    {
        
    }
}
