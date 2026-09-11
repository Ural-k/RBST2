using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : NetworkBehaviour
{

    public static GameStateManager instance;

    public enum GameState
    {
        Title,
        Game,
        Result,
    }

    public GameState state { get; private set; }  //private set ... このスクリプトからのみ変更可能

    private void Awake()
    {
        //シングルトンインスタンス
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //すでにインスタンスがあったら破壊する
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// タイトルシーン呼び出し
    /// </summary>
    public void LordTitle()
    {
        state = GameState.Title;
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// ゲームシーン呼び出し
    /// </summary>
    public void LordGame()
    {
        state = GameState.Game;
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// リザルトシーン呼び出し
    /// </summary>
    public void LordResult()
    {
        state = GameState.Result;
        SceneManager.LoadScene("ResultScene");
    }

    public void ClosedGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
