using System.Linq;
using UnityEngine;

public class AttackCircle : PlayerAttackBase
{
    [SerializeField] private Vector2 radius_ = new Vector2(2, 2);

    public override async void Execute(ITargetCircle from)
    {
        ITargetCircle[] type = targetType_ switch
        {
            TargetType.Enemy => Attack.GetAllEnemy(),
            TargetType.Player => Attack.GetAllPlayer(),
            _ => null
        };

        int num = Mathf.Min(targetNum_, type.Length + 1);

        for (int i = 0; i < num; ++i)
        {
            ITargetCircle target = pivotSet_ switch
            {
                PivotSet.Me => from,
                PivotSet.Near => Vector2.zero,//hitの中から近いターゲットを取得できるようにする
                PivotSet.Far => Vector2.zero,//実装する
                PivotSet.Random => Vector2.zero, //実装する
                _ => from,
            };

            var hit = Attack.GetHitCircle(type, from.GetPosition, radius_, Color.white);
            Attack.TakeDamage(hit, power_);
        }
    }
}
