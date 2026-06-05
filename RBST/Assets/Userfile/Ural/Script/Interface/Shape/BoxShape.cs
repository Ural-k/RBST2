
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoxShape : IAOEshape
{

    public float InnerRadius { get; set; }
    private AOECollect aoeCollect_ = AOECollect.Box;

    public AOECollect AOECollect
    {
        get { return aoeCollect_; }
    }
    public Collider2D[] GetHits(float radius,Vector3 center)
    {
        float size = radius * 2;
        Vector2 scale = new Vector2(size, size);

        return Physics2D.OverlapBoxAll(center, scale,0);
    }


    public void OnDrawGizmos(float radius,Vector3 center)
    {
        float size = radius * 2;
        Vector2 scale = new Vector2(size, size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, scale);
    }
}
