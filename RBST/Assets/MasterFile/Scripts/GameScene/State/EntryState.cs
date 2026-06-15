using UnityEngine;

public class EntryState : IGameState
{
    /// <summary>
    /// EntryStateのstate
    /// </summary>
    private enum EntryPhase
    {
        Phase1,  //プレイヤーを最低一人は参加させるフェーズ
        Phase2,  //ゲームスタートするのを待機するフェーズ
    }

    private EntryPhase phase;
    private IGameState nextState_;  //次のstateを設定しておく
    public void Enter() 
    {
        phase = EntryPhase.Phase1;
        GameUIManager.Instance.Activate(UIType.CharacterSelect);
        nextState_ = new PlayState();
    }

    public void Update() 
    {
        switch (phase)
        {
            case EntryPhase.Phase1:
                if (PlayerManager.GetAllPlayerListCount() > 0)
                {
                    GameUIManager.Instance.Hide(UIType.CharacterSelect);
                    GameUIManager.Instance.Activate(UIType.Ready);
                    phase = EntryPhase.Phase2;
                }
                break;
            case EntryPhase.Phase2:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameSceneManager.Instance.ChangeState(nextState_);
                }
                break;
        }
                
    }
    public void Exit() 
    {
        GameUIManager.Instance.Hide(UIType.Ready);
    }
    
}
