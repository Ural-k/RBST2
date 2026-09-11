using UnityEngine;

public interface INetworkGameState 
{
    /// <summary>
    /// サーバーが呼ぶステート変化時に呼ぶUIメソッド
    /// </summary>
    void ShowInitializeUI();

    /// <summary>
    /// サーバーが呼ぶすべてのUIを隠すメソッド
    /// </summary>
    void HideAllUI();

}
