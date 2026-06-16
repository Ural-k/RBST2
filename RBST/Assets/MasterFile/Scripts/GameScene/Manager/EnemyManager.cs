using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<EnemyControl> enemyList_ = new List<EnemyControl>();

    public static void AddEnemy(EnemyControl enemy)
    {
        if (enemy == null || enemyList_.Contains(enemy)) return;

        enemyList_.Add(enemy);
    }

    public static void DeleteEnemy(EnemyControl enemy)
    {
        int index = enemyList_.IndexOf(enemy);

        if (index >= 0)
        {
            enemyList_[index] = null;
        }
    }


    public static void DeleteAllEnemy()
    {
        enemyList_.Clear();
    }

    public static EnemyControl GetEnemy(EnemyControl enemy)
    {
        foreach (EnemyControl enemyControl in enemyList_)
        {
            if (enemyControl == enemy)
            {
                return enemyControl;
            }
        }

        return null;
    }

    public static EnemyControl GetEnemy(int i)
    {
        if (i < 0 || i >= enemyList_.Count)
        {
            return null;
        }

        return enemyList_[i];
    }

    public static List<EnemyControl> GetAllEnemy()
    {
        return enemyList_;
    }

    public static int GetAllEnemyListCount()
    {
        return enemyList_.Count;
    }

    public static void AllDestroyEnemy()
    {
        enemyList_.Clear();
    }
}