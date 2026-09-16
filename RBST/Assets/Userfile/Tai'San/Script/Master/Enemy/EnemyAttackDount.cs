using System.Drawing;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Donut")]
public class EnemyAttackDonut : EnemyAttackBase
{
    public float innerRadius = 2.0f;
    public float outerRadius = 4.0f;

    public override void TelegraphDuration(Vector3 pos, Transform enemyTransform) 
    {
        AOEManager.Instance.ShowDonut(pos, innerRadius, outerRadius, telegraphColor,telegraphDuration_);
    }
    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitDonut(Attack.GetAllPlayer(), originPosition, innerRadius, outerRadius,AOEColor);
        Attack.TakeDamage(hit, damage_);
    }
}
