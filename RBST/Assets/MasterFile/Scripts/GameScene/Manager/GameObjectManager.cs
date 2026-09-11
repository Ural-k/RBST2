using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectManager : NetworkBehaviour
{
    
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject magic_;
    [SerializeField] private GameObject pharmacist_;
    [SerializeField] private GameObject sword_;
    [SerializeField] private GameObject enemy1_;
    [SerializeField] private GameObject enemy2_;
    [SerializeField] private GameObject enemy3_;
    [SerializeField] private Text bossName_;
    [SerializeField] private GameObject bgmObject_, bgm2_;

    public static GameObjectManager Instance;

    //デバッグ用
    private Vector2 offset_ = new Vector2(5, 0);
    public event Action<ulong> OnPlayerSpawned;
    private readonly HashSet<ulong> spawnedClientIds_ = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CreatePlayer()
    {
        if (IsServer)
        {
            SpawnPlayer(NetworkManager.Singleton.LocalClientId);
        }
        else
        {
            SpawnPlayerServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;
        SpawnPlayer(clientId);
    }

    /// <summary>
    /// ボタンが押されたらプレイヤーを生成させるメソッド
    /// </summary>
    /// <param name="clientId"></param>
    private void SpawnPlayer(ulong clientId)
    {
        //もしその端末が生成済みだったらreturnを返す
        if (spawnedClientIds_.Contains(clientId))
        {
            return;
        }

        GameObject player = Instantiate(
            playerPrefab_,
            Vector3.zero,
            Quaternion.identity
        );

        NetworkObject networkObject = player.GetComponent<NetworkObject>();

        networkObject.SpawnAsPlayerObject(clientId);
        spawnedClientIds_.Add(clientId);

        //プレイヤーが生成されたという通知を飛ばす
        OnPlayerSpawned?.Invoke(clientId);
    }

    /// <summary>
    /// 接続している人すべてがプレイヤーを生成したか判別するメソッド
    /// </summary>
    public bool IsAllConnectedClientsSpawned()
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!spawnedClientIds_.Contains(clientId))
            {
                return false;
            }
        }

        return true;
    }
    public void CreateMagic()
    {
<<<<<<< HEAD
        if (!IsServer) return null;
=======
        Instantiate(magic_).GetComponent<Player>();
    }
    public void CreatePharmacist()
    {
        Player ins = Instantiate(pharmacist_).GetComponent<Player>();
        PlayerManager.AddPlayer(ins);
    }
    public void CreateSword()
    {
        Instantiate(sword_).GetComponent<Player>();
    }

    public Enemy CreateEnemy1()
    {
        var temp = Instantiate(enemy1_,offset,Quaternion.identity);
        Enemy enemy = temp.GetComponent<Enemy>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "RED APPLE";
        bgmObject_.SetActive(true);
>>>>>>> develop

        var temp = Instantiate(enemyPrefab_, offset_, Quaternion.identity);

        NetworkObject networkObject = temp.GetComponent<NetworkObject>();

        if (networkObject != null && !networkObject.IsSpawned)
        {
            networkObject.Spawn();
        }

        return temp.GetComponent<EnemyControl>();
    }

    public Enemy CreateEnemy2()
    {
        var temp = Instantiate(enemy2_, offset, Quaternion.identity);
        Enemy enemy = temp.GetComponent<Enemy>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "GREEN APPLE";

        return enemy;
    }

    public Enemy CreateEnemy3()
    {
        var temp = Instantiate(enemy3_, offset, Quaternion.identity);
        Enemy enemy = temp.GetComponent<Enemy>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "WHITE APPLE";
        bgmObject_.SetActive(false);
        bgm2_.SetActive(true);

        return enemy;
    }

    public void DestroyEnemy(Enemy enemyControl)
    {
        bossName_.text = "NEXT TO ENTER";
        EnemyManager.DeleteEnemy(enemyControl);
    }

    public void ResetPlayersForEntry()
    {
        if (!IsServer) return;

        foreach (Player player in PlayerManager.GetAllPlayer())
        {
            if (player == null) continue;

            NetworkObject networkObject = player.GetComponent<NetworkObject>();

            if (networkObject != null && networkObject.IsSpawned)
            {
                networkObject.Despawn(true);
            }
        }

        spawnedClientIds_.Clear();
        PlayerManager.AllDestroyPlayer();
    }
}
