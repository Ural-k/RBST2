using Unity.Netcode;
using UnityEngine;

public class ResultState : IGameState
{
    private IGameState nextState_;
    private UIType activeUI_;
    public void Enter()
    {
        CheckResult(GameSceneManager.Instance.ResultState);
        GameUIManager.Instance.Activate(activeUI_);
        //nextState_ = new EntryState();
    }

    public void Update()
    {
        // ResultからEntryへ戻せるのはホストだけ
        if (!NetworkManager.Singleton.IsServer) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 次のEntryでまたキャラ生成できるようにリセット
            GameObjectManager.Instance.ResetPlayersForEntry();

            // 全員をEntryStateへ戻す
            GameSceneManager.Instance.ChangeStateByServer(GameSceneStateType.Entry);
        }
    }

    public void Exit() 
    {
        PlayerManager.AllDestroyPlayer();
        EnemyManager.AllDestroyEnemy();
        // Result UIだけ消す
        GameUIManager.Instance.Hide(activeUI_);
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
