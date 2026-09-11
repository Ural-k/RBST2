using Unity.Netcode;
using UnityEngine;

public enum GameState
{
    isPlaying,
    GameOver,
    GameClear,
}

public enum GameSceneStateType
{
    Entry,
    Play,
    Result
}

public class GameSceneManager : NetworkBehaviour
{
    [SerializeField] private NetworkEntryState netEntryState_;
    [SerializeField] private NetworkPlayState playState_;
    public static GameSceneManager Instance;

    private NetworkVariable<GameState> syncedResultState_ = new(
        GameState.isPlaying,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public GameState ResultState
    {
        get { return syncedResultState_.Value; }
    }

    private IGameState currentState_;

    private NetworkVariable<GameSceneStateType> syncedState_ = new(
        GameSceneStateType.Entry,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private void Awake()
    {
        if (Instance == null){ Instance = this;}
    }

    public override void OnNetworkSpawn()
    {
        syncedState_.OnValueChanged += OnStateChanged;

        ChangeStateLocal(syncedState_.Value);
    }

    private void Update()
    {
        //サーバーだけが状態を更新
        if (!IsServer) return;
        currentState_?.Update();
    }

    /// <summary>
    /// すべてのクライアントで起動
    /// </summary>
    private void OnStateChanged(GameSceneStateType oldState,GameSceneStateType newState)
    {
        ChangeStateLocal(newState);
    }

    private void ChangeStateLocal(GameSceneStateType state)
    {
        currentState_?.Exit();
        currentState_ = CreateState(state);
        currentState_?.Enter();
    }

    private IGameState CreateState(GameSceneStateType type)
    {
        switch (type)
        {
            case GameSceneStateType.Entry:
                return new EntryState(netEntryState_);
            case GameSceneStateType.Play:
                return new PlayState(playState_);
            case GameSceneStateType.Result:
                return new ResultState();
            default:
                return new EntryState(netEntryState_);
        }
    }

    public void ChangeStateByServer(GameSceneStateType stateType)
    {
        if (!IsServer) return;

        syncedState_.Value = stateType;
    }

    public void SetResultByServer(GameState result)
    {
        if (!IsServer) return;

        syncedResultState_.Value = result;
    }
}
