using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static List<Player> playerList_ = new List<Player>();  //プレイヤーを管理するリスト

    /// <summary>
    /// プレイヤーをリストに追加する
    /// </summary>
    public static void AddPlayer(Player player)
    {
        playerList_.Add(player);
    }

    /// <summary>
    /// 特定のプレイヤーを削除
    /// </summary>
    public static void DeletePlayer(Player player)
    {
        foreach (Player p in playerList_)
        {
            if (p == player)
            {
                playerList_.Remove(p);
            }
        }
    }
    /// <summary>
    /// list内の特定のPlayerを取得
    /// </summary>
    public static Player GetPlayer(int i)
    {
        if (playerList_.Count == 0) return null;

        return playerList_[i];
    }

    /// <summary>
    /// playerList_の要素数を取得する
    /// </summary>
    public static int GetAllPlayerListCount()
    {  
        return playerList_.Count;
    }
}
