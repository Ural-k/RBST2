using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/Fan")]
public class AttackFan : AttackBase
{
    [SerializeField] private float radius_ = 3.0f;
    [SerializeField] private Vector2 dir_ = Vector2.left;
    [SerializeField] private float angleDeg_ = 45.0f;

    public override async void Execute(ITargetCircle from, Player player)
    {
        //UŒ‚‚³‚ê‚¤‚é‘ÎÛÒ
        ITargetCircle[] attackable = targetType_ switch
        {
            TargetType.Enemy => Attack.GetAllEnemy(),
            TargetType.Player => Attack.GetAllPlayer(),
            _ => null
        };

        if (attackable.Count() == 0) return;

        //ƒ^[ƒQƒbƒg‚Å‚«‚é”
        int num = Mathf.Min(targetNum_, attackable.Length + 1);

        //UŒ‚‚ğ‚·‚é‘ÎÛ
        ITargetCircle[] target = pivotSet_ switch
        {
            PivotSet.Me => new ITargetCircle[] { from },
            PivotSet.Near => Attack.GetNear(attackable, from.GetPosition, num),
            PivotSet.Far => Attack.GetFar(attackable, from.GetPosition, num),
            PivotSet.Random => Attack.GetRandom(attackable, num),
            _ => null,
        };

        List<Vector2> pos = new List<Vector2>();
        foreach(var t in target)
        {
            if (t != null)
            {
                pos.Add(t.GetPosition);
            }
        }

        //’x‰„
        await Task.Delay((int)(delay_ * 1000));

        //UŒ‚
        foreach (var p in pos)
        {
            Vector2 p1 = from.GetPosition; // ©•ª‚ÌˆÊ’u
            Vector2 p2 = p; // ‘Šè‚ÌˆÊ’u

            var hit = Attack.GetHitFan(attackable, from.GetPosition, radius_, p2 - p1, angleDeg_, Color.white);
            Attack.TakeDamage(hit, power_);
        }
    }
}
