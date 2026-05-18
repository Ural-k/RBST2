using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class CircleShape : IAOEshape
{
    public float InnerRadius { get; set; }
    private AOEColect aoeColect_;
    public AOEColect AOEColect
    {
        get { return aoeColect_; }
    }
    public Collider2D[] GetHits(float radius,Vector3 center)
    {
        aoeColect_ = AOEColect.Circle;
        //physics2Dのついているオブジェクトすべてを返す
        return Physics2D.OverlapCircleAll(center,radius);
    }

    public void OnDrawGizmos(float radius,Vector3 center)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
