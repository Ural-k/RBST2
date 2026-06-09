using UnityEngine;

public enum GameState
{
    isPlaying,
    GameOver,
}

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager Instance;
    public GameState State { get;private set; }

    private IGameState currentState_;

    private void Awake()
    {
        currentState_ = new PlayState();
        Instance = this;
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

    public void GameOver()
    {
        State = GameState.GameOver;
    }
}
