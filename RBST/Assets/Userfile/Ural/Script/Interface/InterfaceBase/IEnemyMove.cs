using System.Collections;
using UnityEngine;

public interface IEnemyMove
{
    /// <summary>
    /// どの移動タイプか取得できる
    /// </summary>
    public EnemyMoveCollect moveCollect { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy">enemyの座標</param>
    /// <param name="emStruct">エネミー移動のステータス</param>
    /// <returns></returns>
    public IEnumerator EnemyMoveColutine(Transform enemy, EnemyMoveStract emStruct);
}
