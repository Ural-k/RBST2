using System;
using UnityEngine;

public class PlayState : IGameState
{
    private EnemyControl enemyControl_;
    private IGameState nextState_;
    public void Enter()
    {
        enemyControl_ = GameObjectManager.Instance.CreateEnemy();
        enemyControl_.StartCoroutine(enemyControl_.GimmickCorutine());
        GameUIManager.Instance.SetPlayUI();
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
            GameSceneManager.Instance.ChangeState(nextState_);
        }

    }

    public void Exit() 
    {
        enemyControl_.StopAllCoroutines();
        GameUIManager.Instance.HidePlayUI();
    }
}
