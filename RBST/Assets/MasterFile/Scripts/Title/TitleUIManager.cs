using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            GameStateManager.instance.LordGame();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            GameStateManager.instance.LordGame();
        });
    }
}
