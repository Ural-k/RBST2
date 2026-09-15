using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Donut")]
public class EnemyAttackDonut : EnemyAttackBase
{
    public float innerRadius = 2.0f;
    public float outerRadius = 4.0f;

    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitDonut(Attack.GetAllPlayer(), originPosition, innerRadius, outerRadius);
        Attack.TakeDamage(hit, damage_);
    }
}
