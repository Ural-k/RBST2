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
        enemyList_.Remove(enemy);
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

    public static void DamageEnemy(EnemyControl enemy, int damage)
    {
        EnemyControl target = GetEnemy(enemy);
        if (target == null) return;

        target.TakeDamage(damage);
    }

    public static void HealEnemy(EnemyControl enemy, int heal)
    {
        EnemyControl target = GetEnemy(enemy);
        if (target == null) return;

        target.Heal(heal);
    }

    public static void AllDestroyEnemy()
    {
        enemyList_.Clear();
    }
}