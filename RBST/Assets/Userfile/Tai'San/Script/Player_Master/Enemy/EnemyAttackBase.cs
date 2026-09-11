using UnityEngine;

public abstract class EnemyAttackBase : ScriptableObject
{
    public float telegraphDuration = 0.5f; // 予兆
    public int damage;

    //実際の攻撃処理
    public abstract void Execute(Vector3 originPosition, Transform enemyTransform);
}