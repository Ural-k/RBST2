using System.Drawing;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttacks/Rect")]
public class EnemyAttackRect : EnemyAttackBase
{
    public Vector2 size_;
    public float deg_;

    private Vector2 targetPos_;
    public override void TelegraphDuration(Vector3 pos, Transform enemyTransform)
    {
        targetPos_ = Attack.GetNearPlayerPos(pos);
        AOEManager.Instance.ShowRectangle(targetPos_, size_, deg_, telegraphColor,telegraphDuration_);
    }
    public override void Execute(Vector3 originPosition, Transform enemyTransform)
    {
        var hit = Attack.GetHitRect(Attack.GetAllPlayer(), targetPos_, size_, AOEColor,deg_);
        Attack.TakeDamage(hit, damage_);
    }
}
