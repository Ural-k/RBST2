using System;
using UnityEngine;

public class PlayState : IGameState
{
    private EnemyControl enemyControl_;
    private IGameState nextState_;
    private int playerCount_;
    private Player[] players;
    public void Enter()
    {
        playerCount_ = PlayerManager.GetAllPlayerListCount();
        for (int i = 0; i < playerCount_; i++)
        {
            players[i] = PlayerManager.GetPlayer(i).GetComponent<Player>();
        }
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

        if (enemyControl_.HP <= 0)
        {
            if (enemyControl_ != null)
            {
                enemyControl_.Died();
            }
            GameSceneManager.Instance.ChangeState(nextState_);
        }
    }

    public void Exit() 
    {
        GameUIManager.Instance.Hide(UIType.Play);
    }
}
