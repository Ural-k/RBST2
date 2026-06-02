using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<EnemyControl> enemyList_ = new List<EnemyControl>();
   
    /// <summary>
    /// エネミーの追加
    /// </summary>
    /// <param name="enemy"></param>
    public static void AddEnemy(EnemyControl enemy)
    {
        enemyList_.Add(enemy);
    }

    /// <summary>
    /// 特定のエネミー削除
    /// </summary>
    public static void DeleteEnemy(EnemyControl enemy)
    {
        foreach(EnemyControl enemyControl in enemyList_)
        {
            if(enemyControl == enemy)
            {
                enemyList_.Remove(enemyControl);
            }
        }
    }

    /// <summary>
    /// 全敵の削除
    /// </summary>
    public static void DeleteAllEnemy()
    {
        enemyList_.Clear();
    }

    /// <summary>
    /// エネミーの取得
    /// </summary>
    public static EnemyControl GetEnemy(int i)
    {
        return enemyList_[i];
    }
    
    
}
