using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/Circle")]
public class AttackCircle : AttackBase
{
    [SerializeField] private Vector2 radius_ = new(2, 2);

    public override async void Execute(ITargetCircle from, Player player)
    {
        //çUåÇÇ≥ÇÍÇ§ÇÈëŒè€é“
        ITargetCircle[] attackable = targetType_ switch
        {
            TargetType.Enemy => Attack.GetAllEnemy(),
            TargetType.Player => Attack.GetAllPlayer(),
            TargetType.All => Attack.GetAllEntity(),
            _ => null
        };

        if (attackable.Count() == 0) return;

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

        List<Vector2> pos = new List<Vector2>();
        foreach (var t in target)
        {
            if (t != null)
            {
                pos.Add(t.GetPosition);
            }
        }

        //íxâÑ
        await Task.Delay((int)(delay_ * 1000));

        //çUåÇ
        foreach (var p in pos)
        {
            var hit = Attack.GetHitCircle(attackable, p, radius_, Color.white);
            Attack.TakeDamage(hit, power_);
        }
    }
}
