using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class CircleShape : IAOEshape
{
    public Collider2D[] GetHits(float radius,Vector2 center)
    {
        //physics2Dのついているオブジェクトすべてを返す
        return Physics2D.OverlapCircleAll(center,radius);
    }

    public void OnDrawGizmos(float radius,Vector2 center)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
