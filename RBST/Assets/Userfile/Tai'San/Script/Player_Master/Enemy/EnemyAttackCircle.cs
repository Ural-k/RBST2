using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Circle")]
public class EnemyAttackCircle : EnemyAttackBase
{
    public Vector2 radius = new Vector2(2, 2);

    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitCircle(Attack.GetAllPlayer(), originPosition, radius);
        Attack.TakeDamage(hit, damage);
    }
}
