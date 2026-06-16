using UnityEngine;

public enum GameState
{
    isPlaying,
    GameOver,
    GameClear,
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    public GameState State { get; set; }

    private IGameState currentState_;

    private void Awake()
    {
        if (Instance == null){ Instance = this;}
    }

    private void Start()
    {
        currentState_ = new EntryState();
        currentState_?.Enter();
    }

    private void Update()
    {
       currentState_?.Update();
    }

    public void ChangeState(IGameState state)
    {
        currentState_?.Exit();
        currentState_ = state;
        currentState_?.Enter();
    }
}
