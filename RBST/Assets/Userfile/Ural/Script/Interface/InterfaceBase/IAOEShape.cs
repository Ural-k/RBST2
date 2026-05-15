using UnityEngine;

public interface IAOEshape 
{
    Collider2D[] GetHits(float radian, Vector2 center);
    public void OnDrawGizmos(float radius, Vector2 center);
}
