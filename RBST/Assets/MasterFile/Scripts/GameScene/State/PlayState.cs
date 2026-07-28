using System;
using System.Collections;
using UnityEngine;

public class PlayState : IGameState
{
    private EnemyControl enemyControl_;
    private IGameState nextState_;
    private int playerCount_;
    private GetPlayUI playUI_;
    public void Enter()
    {
        playerCount_ = PlayerManager.GetAllPlayerListCount();
        enemyControl_ = GameObjectManager.Instance.CreateEnemy1();
        GameUIManager.Instance.Activate(UIType.Play);
        playUI_ = GameUIManager.Instance.GetPlayUI();
        nextState_ = new ResultState();
        DemoTimer.Instance.ResetTimer();
        DemoTimer.Instance.StartTimer();
    }

    private int demoNowEnemy_ = 0;
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
            EnemyManager.AllDestroyEnemy();
            switch (demoNowEnemy_)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ++demoNowEnemy_;
                        enemyControl_ = GameObjectManager.Instance.CreateEnemy2();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ++demoNowEnemy_;
                        enemyControl_ = GameObjectManager.Instance.CreateEnemy3();
                    }
                    break;
                case 2:
                    Clear();
                    break;
            }


            //Clear();
        }

        //蘇生作成後再調整
        for (int i = 0; i < playerCount_; i++)
        {
            playUI_.SetText(PlayerManager.GetPlayer(i));
            if(PlayerManager.GetPlayerHP(i) <= 0 || DemoTimer.Instance.GetCurrentTime <= 0)
            {
                GameOver(i);
            }
        }
    }

    public void Exit() 
    {
        EnemyManager.AllDestroyEnemy();
        GameUIManager.Instance.Hide(UIType.Play);
    }

    private void Clear()
    {
        if (enemyControl_ != null)
        {
            enemyControl_.Died();
        }
        DemoTimer.Instance.StopTimer();
        GameSceneManager.Instance.State = GameState.GameClear;
        GameSceneManager.Instance.ChangeState(nextState_);
    }

    private void GameOver(int i)
    {
        enemyControl_.StopAllCoroutines();
        DemoTimer.Instance.StopTimer();
        PlayerManager.DestroyPlayer(PlayerManager.GetPlayer(i));
        GameSceneManager.Instance.State = GameState.GameOver;
        GameSceneManager.Instance.ChangeState(nextState_);
    }
}
