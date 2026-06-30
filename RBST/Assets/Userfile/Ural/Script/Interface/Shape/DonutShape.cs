using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CircleShape : IAOEshape
{
    private AOECollect aoeCollect_ = AOECollect.Circle;

    public AOECollect AOECollect
    {
        get { return aoeCollect_; }
    }

    public Collider2D[] GetHits(EnemyAttackStract enemyAttackStract, Vector3 center)
    {

        float radius = enemyAttackStract.scale.x / 2;
        var hits = Physics2D.OverlapCircleAll(center, radius);
        List<Collider2D> result = new List<Collider2D>();

        foreach (var hit in hits)
        {
            float sqrDist = (hit.transform.position - center).sqrMagnitude;
            if (sqrDist >= enemyAttackStract.innerRadius * enemyAttackStract.innerRadius)
            {
                result.Add(hit);
            }
        }

        return result.ToArray();
    }

   public void OnDrawGizmos(EnemyAttackStract enemyAttackStract, Vector3 center, Matrix4x4 matrix4)
    {

        float radius = enemyAttackStract.scale.x / 2;
        Gizmos.color = Color.yellow;

        // ŠO‘¤
        Gizmos.DrawWireSphere(center, radius);

        // “à‘¤
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(center, enemyAttackStract.innerRadius);
    }
}


