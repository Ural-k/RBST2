
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoxShape : IAOEshape
{
    private AOECollect aoeCollect_ = AOECollect.Box;

    public AOECollect AOECollect
    {
        get { return aoeCollect_; }
    }
    public Collider2D[] GetHits(EnemyAttackStract enemyAttackStract, Vector3 center)
    {
        Vector2 scale = enemyAttackStract.scale;
        float angle = enemyAttackStract.angle;
        return Physics2D.OverlapBoxAll(center, scale,angle);
    }


    public void OnDrawGizmos(EnemyAttackStract enemyAttackStract, Vector3 center , Matrix4x4 matrix4)
    {
        Vector2 scale = enemyAttackStract.scale;
        Gizmos.color = Color.green;
        Gizmos.matrix = matrix4;
        Gizmos.DrawWireCube(Vector3.zero, scale);
    }
}
