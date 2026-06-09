using UnityEngine;

public enum GameState
{
    isPlaying,
    GameOver,
}

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager Instance;
    [SerializeField] private Canvas result_;
    public GameState State { get;private set; }

    private void Awake()
    {
        Instance = this;
        result_.gameObject.SetActive(false);
    }

    private void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.ClosedGame();
        }
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        result_.gameObject.SetActive(true);
    }
}
