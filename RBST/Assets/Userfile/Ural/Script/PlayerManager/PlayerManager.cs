using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Player temp = null;
        foreach (Player p in playerList_)
        {
            if (p == player)
            {
                temp = p;
            }
        }

        playerList_.Remove(temp);
    }
    /// <summary>
    /// list内の特定のPlayerを取得
    /// </summary>
    public static Player GetPlayer(int i)
    {
        if (i < 0 || i >= playerList_.Count) return null;

        return playerList_[i];
    }

    /// <summary>
    /// playerList_の要素数を取得する
    /// </summary>
    public static int GetAllPlayerListCount()
    {  
        return playerList_.Count;
    }

    public static void AllDestroyPlayer()
    {
        playerList_.Clear();
    }

    public static List<Player> GetAllPlayer()
    {
        return playerList_;
    }

    /// <summary>
    /// 仮
    /// </summary>
    public static void DestroyPlayer(Player player)
    {
        Player temp = null;
        foreach (Player p in playerList_)
        {
            if (p == player)
            {
                temp = p;
            }
        }

        Destroy(temp.gameObject);
    }


    public static float GetPlayerHP(int i)
    {
        return playerList_[i].Parameter.hp_;
    }

}
