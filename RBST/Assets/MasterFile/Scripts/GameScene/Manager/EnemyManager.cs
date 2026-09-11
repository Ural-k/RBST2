using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<Enemy> enemyList_ = new List<Enemy>();

    public static void AddEnemy(Enemy enemy)
    {
        if (enemy == null || enemyList_.Contains(enemy)) return;

        enemyList_.Add(enemy);
    }

    public static void DeleteEnemy(Enemy enemy)
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

    public static Enemy GetEnemy(Enemy enemy)
    {
        foreach (Enemy enemyControl in enemyList_)
        {
            if (enemyControl == enemy)
            {
                return enemyControl;
            }
        }

        return null;
    }

    public static Enemy GetEnemy(int i)
    {
        if (i < 0 || i >= enemyList_.Count) return null;

        return enemyList_[i];
    }

    public static List<Enemy> GetAllEnemy()
    {
        return enemyList_;
    }

    public static int GetAllEnemyListCount()
    {
        return enemyList_.Count;
    }

    public static void AllDestroyEnemy()
    {
        foreach (EnemyControl enemy in enemyList_)
        {
            if (enemy == null) continue;

            NetworkObject networkObject = enemy.GetComponent<NetworkObject>();

            if (networkObject != null && networkObject.IsSpawned)
            {
                networkObject.Despawn(true);
            }
            else
            {
                UnityEngine.Object.Destroy(enemy.gameObject);
            }
        }

        enemyList_.Clear();
    }
}