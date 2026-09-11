using UnityEngine;

public class NetworkPlayState : MonoBehaviour , INetworkGameState
{
    public void ShowInitializeUI()
    {
        GameUIManager.Instance.Activate(UIType.Play);
    }

    public void HideAllUI()
    {
        GameUIManager.Instance.Hide(UIType.Play);
    }
}
