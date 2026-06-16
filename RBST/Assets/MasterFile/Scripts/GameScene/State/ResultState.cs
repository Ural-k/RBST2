using UnityEngine;

public class ResultState : IGameState
{
    private IGameState nextState_;
    private UIType activeUI_;
    public void Enter()
    {
        CheckResult(GameSceneManager.Instance.State);
        GameUIManager.Instance.Activate(activeUI_);
        nextState_ = new EntryState();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameUIManager.Instance.Hide(activeUI_);
            Exit();
        }
    }

    public void Exit() 
    {
        PlayerManager.AllDestroyPlayer();
        EnemyManager.AllDestroyEnemy();
        GameStateManager.instance.LordTitle();
    }
    private void CheckResult(GameState state)
    {
        switch (state)
        {
            case GameState.GameClear:
                activeUI_ = UIType.GameClear;
                break;
            case GameState.GameOver:
                activeUI_ = UIType.GameOver;
                break;
        }
    }

}
