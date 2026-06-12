using UnityEngine;

public class ResultState : IGameState
{
    private IGameState nextState_;
    public void Enter()
    {
        GameUIManager.Instance.Activate(UIType.Result);
        nextState_ = new EntryState();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameUIManager.Instance.Hide(UIType.Result);
            Exit();
            
        }
    }

    public void Exit() 
    {
        PlayerManager.AllDestroyPlayer();
        EnemyManager.AllDestroyEnemy();
        GameStateManager.instance.LordTitle();
    }
}
