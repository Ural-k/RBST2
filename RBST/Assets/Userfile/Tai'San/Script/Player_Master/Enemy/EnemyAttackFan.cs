using System.Drawing;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Fan")]
public class EnemyAttackFan : EnemyAttackBase
{
    public float radius = 3.0f;
    public Vector2 dir = Vector2.left;
    public float angleDeg = 45.0f;

    public override void TelegraphDuration(Vector3 pos, Transform enemyTransform) 
    {
        AOEManager.Instance.ShowFan(pos, angleDeg, dir, radius, telegraphColor,telegraphDuration_);
    }

    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitFan(Attack.GetAllPlayer(), originPosition, radius, Vector2.left, angleDeg,AOEColor);
        Attack.TakeDamage(hit, damage_);
    }
}
