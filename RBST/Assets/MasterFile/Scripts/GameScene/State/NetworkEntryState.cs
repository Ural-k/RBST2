using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class NetworkEntryState : NetworkBehaviour ,INetworkGameState
{
    private bool isListeningPlayerSpawned_;

    public void ShowInitializeUI()
    {
        GameUIManager.Instance.Activate(UIType.CharacterSelect);
        GameUIManager.Instance.Hide(UIType.Ready);
    }

    public void HideAllUI()
    {
        if (!IsServer) return;

        HideEntryUIClientRpc();
    }

    /// <summary>
    /// EntryStateがPhase2になったときに呼ばれるUI
    /// </summary>
    public void ShowReadyUI()
    {
        if (!IsServer) return;

        //ShowReadyUIClientRpc();
    }

    //EntryState中GameObjectManagerからの通知を受け取る処理
    public void BeginListenPlayerSpawned()
    {
        if (isListeningPlayerSpawned_) return;

        GameObjectManager.Instance.OnPlayerSpawned += HandlePlayerSpawned;
        isListeningPlayerSpawned_ = true;
    }

    //EntryStateが終わったら通知を受け取らなくする
    public void EndListenPlayerSpawned()
    {
        if (!isListeningPlayerSpawned_) return;
        if (GameObjectManager.Instance == null) return;

        GameObjectManager.Instance.OnPlayerSpawned -= HandlePlayerSpawned;
        isListeningPlayerSpawned_ = false;
    }

    private void HandlePlayerSpawned(ulong clientId)
    {
        if (!IsServer) return;

        ShowReadyTargetClientRpc(
            new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new[] { clientId }
                }
            }
        );
    }

    /// <summary>
    /// クライアント側が呼ぶキャラクターセレクトUI表示メソッド
    /// </summary>
    [ClientRpc]
    private void ShowCharacterSelectClientRpc()
    {
        GameUIManager.Instance.Activate(UIType.CharacterSelect);
        GameUIManager.Instance.Hide(UIType.Ready);
    }

    /// <summary>
    /// クライアント側が呼ぶ準備状態のUI表示メソッド
    /// </summary>
    [ClientRpc]
    private void ShowReadyTargetClientRpc(ClientRpcParams clientRpcParams = default)
    {
        GameUIManager.Instance.Hide(UIType.CharacterSelect);
        GameUIManager.Instance.Activate(UIType.Ready);
    }

    /// <summary>
    /// クライアント側が呼ぶすべてのUIを隠すメソッド
    /// </summary>
    [ClientRpc]
    private void HideEntryUIClientRpc()
    {
        GameUIManager.Instance.Hide(UIType.CharacterSelect);
        GameUIManager.Instance.Hide(UIType.Ready);
    }


}

