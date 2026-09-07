using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Rect")]
public class EnemyAttackRect : EnemyAttackBase
{
    public Vector2 size_;
    public float deg_;

    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitRect(Attack.GetAllPlayer(), Attack.GetNearPlayerPos(originPosition), size_, deg_);
        Attack.TakeDamage(hit, damage);
    }
}
