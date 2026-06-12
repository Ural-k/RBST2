using System;
using UnityEngine;

public class PlayState : IGameState
{
    private EnemyControl enemyControl_;
    private IGameState nextState_;
    public void Enter()
    {
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            enemyControl_.Died();
            GameSceneManager.Instance.ChangeState(nextState_);
        }

    }

    public void Exit() 
    {
        GameUIManager.Instance.Hide(UIType.Play);
    }
}
