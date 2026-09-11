using Unity.Netcode;
using UnityEngine;

public class EntryState : IGameState
{
    private readonly NetworkEntryState networkEntryState_;
    /// <summary>
    /// EntryStateのstate
    /// </summary>
    private enum EntryPhase
    {
        Phase1,  //マッチングフェーズ
        Phase2,  //プレイヤーを最低一人は参加させるフェーズ
        Phase3,  //ゲームスタートするのを待機するフェーズ
    }

    private EntryPhase phase_;
    private GameSceneStateType nextState_;  //次のstateを設定しておく

    public EntryState(NetworkEntryState network)
    {
        networkEntryState_ = network;
    }
    public void Enter() 
    {
        GameSceneManager.Instance.SetResultByServer(GameState.isPlaying);
        phase_ = EntryPhase.Phase2;
        nextState_ = GameSceneStateType.Play;

        networkEntryState_.BeginListenPlayerSpawned();
        networkEntryState_.ShowInitializeUI();
        GameSceneManager.Instance.State = GameState.isPlaying;
        phase = EntryPhase.Phase2;//一旦マッチングフェーズを飛ばす
        GameUIManager.Instance.Activate(UIType.CharacterSelect);
        nextState_ = new PlayState();
    }

    public void Update() 
    {
        switch (phase_)
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

                if (GameObjectManager.Instance.IsAllConnectedClientsSpawned())
                {
                    phase_ = EntryPhase.Phase3;
                }
                break;
            case EntryPhase.Phase3:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameSceneManager.Instance.ChangeStateByServer(nextState_);
                }
                break;
        }
                
    }
    public void Exit() 
    {
        networkEntryState_.EndListenPlayerSpawned();
        networkEntryState_.HideAllUI();
    }

    
    
}
