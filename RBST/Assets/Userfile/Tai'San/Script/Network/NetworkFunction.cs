using Unity.Netcode;
using UnityEngine;

public class NetworkFunction : MonoBehaviour
{
    /// <summary>
    /// ホスト状態でスタート
    /// </summary>
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    /// <summary>
    /// クライアント状態でスタート
    /// </summary>
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
