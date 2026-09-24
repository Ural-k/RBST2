using UnityEngine;

public class PlayerAttackCircle : PlayerAttackBase
{
    [SerializeField] private Vector2 radius = new Vector2(2, 2);

    public override async void Execute(Vector2 originPosition)
    {
        //ITargetCircle[] hit;
        //switch (pivotType_)
        //{
        //    case TargetType.Me:
        //        hit = Attack.GetHitCircle()
        //        break;
        //}
        //var hit = Attack.GetHitCircle(Attack.GetAllPlayer(), );
        //Attack.TakeDamage(hit, damage_);
    }
}
