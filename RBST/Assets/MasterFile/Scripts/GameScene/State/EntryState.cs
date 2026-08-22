using Unity.Netcode;
using UnityEngine;

public class EntryState : IGameState
{
    /// <summary>
    /// EntryStateのstate
    /// </summary>
    private enum EntryPhase
    {
        Phase1,  //マッチングフェーズ
        Phase2,  //プレイヤーを最低一人は参加させるフェーズ
        Phase3,  //ゲームスタートするのを待機するフェーズ
    }

    private EntryPhase phase;
    private IGameState nextState_;  //次のstateを設定しておく
    public void Enter() 
    {
        GameSceneManager.Instance.State = GameState.isPlaying;
        phase = EntryPhase.Phase2;//一旦マッチングフェーズを飛ばす
        GameUIManager.Instance.Activate(UIType.CharacterSelect);
        nextState_ = new PlayState();
    }

    public void Update() 
    {
        switch (phase)
        {
            case EntryPhase.Phase1:
                //ネットが接続されているか
                if(Application.internetReachability != NetworkReachability.NotReachable)
                {

                }
                if (NetworkManager.Singleton.IsConnectedClient)
                {

                }
                break;
            case EntryPhase.Phase2:
                if (PlayerManager.GetAllPlayerListCount() > 0)
                {
                    GameUIManager.Instance.Hide(UIType.CharacterSelect);
                    GameUIManager.Instance.Activate(UIType.Ready);
                    phase = EntryPhase.Phase3;
                }
                break;
            case EntryPhase.Phase3:
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
