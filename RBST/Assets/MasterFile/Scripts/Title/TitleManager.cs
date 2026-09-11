using Unity.Netcode;
using UnityEngine;

public class TitleManager : NetworkBehaviour
{

    [SerializeField] NetworkObject gameStateManager;

    private void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LordGameScene()
    {
        GameStateManager.instance.LordGame();
    }
    public void ExitGame()
    {
        Debug.Log("A");
        GameStateManager.instance.ClosedGame();
    }
}
