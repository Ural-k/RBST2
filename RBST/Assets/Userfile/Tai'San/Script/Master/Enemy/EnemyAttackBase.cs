using UnityEngine;

public abstract class EnemyAttackBase : ScriptableObject
{
    public float telegraphDuration_ = 0.5f; // 予兆
    public int damage_;

    /// <summary>
    /// 予兆処理
    /// </summary>
    /// <param name="pos"></param>
    //public abstract void TelegraphDuration(Vector2 pos);

    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="enemyTransform"></param>
    public abstract void Execute(Vector3 pos, Transform enemyTransform);
}