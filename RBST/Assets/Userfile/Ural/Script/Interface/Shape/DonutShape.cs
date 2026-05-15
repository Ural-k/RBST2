using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DounutShape : IAOEshape
{
    private float innerRadius;

    public Collider2D[] GetHits(float radius, Vector2 center)
    {
        return GetHits(innerRadius, radius, center);
    }
    public Collider2D[] GetHits(float innerRadius, float outerRadius, Vector2 center)
    {

        var hits = Physics2D.OverlapCircleAll(center, outerRadius);
        List<Collider2D> result = new List<Collider2D>();

        foreach (var hit in hits)
        {
            float sqrDist = ((Vector2)hit.transform.position - center).sqrMagnitude;
            if (sqrDist >= innerRadius * innerRadius)
            {
                result.Add(hit);
            }
        }

        return result.ToArray();
    }

   public void OnDrawGizmos(float radius,Vector2 center)
    {
        Gizmos.color = Color.yellow;

        // ŠO‘¤
        Gizmos.DrawWireSphere(center, radius);

        // “à‘¤
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(center, innerRadius);
    }
}
