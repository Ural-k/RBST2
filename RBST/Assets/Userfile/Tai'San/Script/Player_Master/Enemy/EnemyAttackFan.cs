using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Fan")]
public class EnemyAttackFan : EnemyAttackBase
{
    public float radius = 3.0f;
    public Vector2 dir = Vector2.left;
    public float angledeg = 45.0f;

    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitFan(Attack.GetAllPlayer(), originPosition, radius, Vector2.left, angledeg);
        Attack.TakeDamage(hit, damage);
    }
}
