using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager instance_ = new();

    private List<Player> playerList_ = new List<Player>();
    private List<TestEnemy> enemyList_ = new List<TestEnemy>();

    public static void AddPlayer(Player player) => instance_.playerList_.Add(player);
    public static void AddEnemy(TestEnemy enemy) => instance_.enemyList_.Add(enemy);

    public static List<Player> GetAllPlayer() => instance_.playerList_;
    public static List<TestEnemy> GetAllEnemy() => instance_.enemyList_;

    public static int GetPlayerCount() => instance_.playerList_.Count;
    public static int GetEnemyCount() => instance_.enemyList_.Count;
}
