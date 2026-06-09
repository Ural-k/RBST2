using UnityEngine;

public class PlayState : IGameState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.ClosedGame();
        }
    }

    public void Exit() 
    { 
        
    }
}
