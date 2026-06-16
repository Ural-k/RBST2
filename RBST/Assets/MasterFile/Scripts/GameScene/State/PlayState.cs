using System;
using UnityEngine;

public class PlayState : IGameState
{
    private EnemyControl enemyControl_;
    private IGameState nextState_;
    private int playerCount_;
    public void Enter()
    {
        playerCount_ = PlayerManager.GetAllPlayerListCount();
        enemyControl_ = GameObjectManager.Instance.CreateEnemy();
        GameUIManager.Instance.Activate(UIType.Play);
        nextState_ = new ResultState();
    }

    // Update is called once per frame
    public void Update()
    {
        
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.ClosedGame();
        }

        //複数戦作成時再調整
        if (enemyControl_.HP <= 0)
        {
            if (enemyControl_ != null)
            {
                enemyControl_.Died();
            }
            GameSceneManager.Instance.State = GameState.GameClear;
            GameSceneManager.Instance.ChangeState(nextState_);
        }

        //蘇生作成後再調整
        for (int i = 0; i < playerCount_; i++)
        {
            if(PlayerManager.GetPlayerHP(i) <= 0)
            {
                enemyControl_.StopAllCoroutines();
                PlayerManager.DestroyPlayer(PlayerManager.GetPlayer(i));
                GameSceneManager.Instance.State = GameState.GameOver;
                GameSceneManager.Instance.ChangeState(nextState_);
            }
        }
    }

    public void Exit() 
    {
        GameUIManager.Instance.Hide(UIType.Play);
    }
}
