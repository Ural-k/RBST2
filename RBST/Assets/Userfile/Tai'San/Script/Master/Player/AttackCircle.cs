using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/Circle")]
public class AttackCircle : AttackBase
{
    [SerializeField] private Vector2 radius_ = new Vector2(2, 2);

    public override async void Execute(ITargetCircle from)
    {
        //çUåÇÇ≥ÇÍÇ§ÇÈëŒè€é“
        ITargetCircle[] attackable = targetType_ switch
        {
            TargetType.Enemy => Attack.GetAllEnemy(),
            TargetType.Player => Attack.GetAllPlayer(),
            _ => null
        };

        //É^Å[ÉQÉbÉgÇ≈Ç´ÇÈêî
        int num = Mathf.Min(targetNum_, attackable.Length + 1);

        //çUåÇÇÇ∑ÇÈëŒè€
        ITargetCircle[] target = pivotSet_ switch
        {
            PivotSet.Me => new ITargetCircle[] { from },
            PivotSet.Near => Attack.GetNear(attackable, from.GetPosition, num),
            PivotSet.Far => Attack.GetFar(attackable, from.GetPosition, num),
            PivotSet.Random => Attack.GetRandom(attackable, num),
            _ => null,
        };

        //çUåÇ
        foreach (var t in target)
        {
            if (t != null)
            {
                var hit = Attack.GetHitCircle(attackable, t.GetPosition, radius_, Color.white);
                Attack.TakeDamage(hit, power_);
            }
        }
    }
}
