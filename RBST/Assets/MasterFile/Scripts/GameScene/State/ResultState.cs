using UnityEditorInternal;
using UnityEngine;

public class ResultState : IGameState
{
    public void Enter()
    {
        GameSceneManager.Instance.GameOver();
        GameUIManager.Instance.SetResultUI();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameStateManager.instance.LordTitle();
        }
    }

    public void Exit() 
    { 
        GameUIManager.Instance.HideResultUI();
        
    }
}
