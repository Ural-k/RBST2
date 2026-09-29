using UnityEngine;

public class TitleManager : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LordGameScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
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
