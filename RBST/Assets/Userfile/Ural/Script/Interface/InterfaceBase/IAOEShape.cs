using UnityEngine;

public interface IAOEshape
{

    /// <summary>
    /// 各AOEが自分がどの形か記憶出来るようにしておく
    /// </summary>
    public AOECollect AOECollect { get; }

    /// <summary>
    /// AOEのステータスを渡して当たり判定を生成する
    /// </summary>
    /// <param name="enemyAttackStract">AOEのステータス</param>
    /// <param name="center">AOEの中心点</param>
    /// <returns></returns>
    Collider2D[] GetHits(EnemyAttackStract enemyAttackStract,Vector3 center);

    /// <summary>
    /// デバッグ用の当たり判定可視化
    /// </summary>
#if UNITY_EDITOR
    public void OnDrawGizmos(EnemyAttackStract enemyAttackStract, Vector3 center,Matrix4x4 matrix4);
#endif
}
