using Unity.Netcode;
using UnityEngine;

public class MultiManager : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        /*
         :  ホスト・クライアントとして接続されたときにプレイヤーをスポーンさせる
         :  ※この状態だとプレイヤーインプットは対応していない
         */
        Debug.Log("接続");
    }

    public void ShowRoom()
    {
        //ネットに接続可能
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Roomリスト表示可能");
        }
    }

    /// <summary>
    /// ホスト状態で接続する
    /// </summary>
    public static void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    /// <summary>
    /// クライアント状態でスタート
    /// </summary>
    public static void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

}
