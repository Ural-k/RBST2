using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DounutShape : IAOEshape
{
    private float innerRadius_;
    private AOEColect aoeColect_;

    public float InnerRadius { get { return innerRadius_; }  set { innerRadius_ = value; } }

    public AOEColect AOEColect
    {
        get { return aoeColect_; }
    }
    public Collider2D[] GetHits(float radius, Vector3 center)
    {
        aoeColect_ = AOEColect.Donut;

        var hits = Physics2D.OverlapCircleAll(center, radius);
        List<Collider2D> result = new List<Collider2D>();

        foreach (var hit in hits)
        {
            float sqrDist = ((Vector3)hit.transform.position - center).sqrMagnitude;
            if (sqrDist >= innerRadius_ * innerRadius_)
            {
                result.Add(hit);
            }
        }

        return result.ToArray();
    }

   public void OnDrawGizmos(float radius,Vector3 center)
    {
        Gizmos.color = Color.yellow;

        // ŠO‘¤
        Gizmos.DrawWireSphere(center, radius);

        // “à‘¤
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(center, innerRadius_);
    }
}


