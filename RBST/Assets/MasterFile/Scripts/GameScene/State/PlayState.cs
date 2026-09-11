using System;
<<<<<<< HEAD
using Unity.Netcode;
=======
using System.Collections;
>>>>>>> develop
using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
<<<<<<< HEAD
    private EnemyControl enemyControl_;
    private GameSceneStateType nextState_;
=======
    [SerializeField] Text text_;
    private Enemy enemyControl_;
    private IGameState nextState_;
>>>>>>> develop
    private int playerCount_;
    private GetPlayUI playUI_;
    private NetworkPlayState playState_;

    public PlayState(NetworkPlayState playState)
    {
        playState_ = playState;
    }
    public void Enter()
    {
        playerCount_ = PlayerManager.GetAllPlayerListCount();
<<<<<<< HEAD

        if (NetworkManager.Singleton.IsServer)
        {
            enemyControl_ = GameObjectManager.Instance.CreateEnemy();
        }

=======
        enemyControl_ = GameObjectManager.Instance.CreateEnemy1();
>>>>>>> develop
        GameUIManager.Instance.Activate(UIType.Play);
        playUI_ = GameUIManager.Instance.GetPlayUI();
        nextState_ = GameSceneStateType.Result;
        DemoTimer.Instance.ResetTimer();
        DemoTimer.Instance.StartTimer();
    }

    private int demoNowEnemy_ = 0;
    // Update is called once per frame
    public void Update()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.ClosedGame();
        }

        //複数戦作成時再調整
        if (enemyControl_.Parameter.hp_ <= 0)
        {
            DemoTimer.Instance.StopTimer();
            EnemyManager.AllDestroyEnemy();
            switch (demoNowEnemy_)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ++demoNowEnemy_;
                        enemyControl_ = GameObjectManager.Instance.CreateEnemy2();
                        DemoTimer.Instance.ResetTimer();
                        DemoTimer.Instance.StartTimer();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ++demoNowEnemy_;
                        enemyControl_ = GameObjectManager.Instance.CreateEnemy3();
                        DemoTimer.Instance.ResetTimer();
                        DemoTimer.Instance.StartTimer();
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
        }

        if (IsAllPlayersDead() || DemoTimer.Instance.GetCurrentTime <= 0)
        {
            GameOver();
        }

    }

    public void Exit()
    {
        GameUIManager.Instance.Hide(UIType.Play);
    }

    private bool IsAllPlayersDead()
    {
        int playerCount = PlayerManager.GetAllPlayerListCount();

        if (playerCount <= 0) return false;

        for (int i = 0; i < playerCount; i++)
        {
            Player player = PlayerManager.GetPlayer(i);

            if (player == null) continue;

            if (player.GetInfo.parameter_.HP > 0)
            {
                return false;
            }
        }

        return true;
    }

    private void Clear()
    {
        if (enemyControl_ != null)
        {
            enemyControl_.Died();
        }
        DemoTimer.Instance.StopTimer();
        GameSceneManager.Instance.SetResultByServer(GameState.GameClear);
        GameSceneManager.Instance.ChangeStateByServer(nextState_);
    }

    private void GameOver()
    {
        if (enemyControl_ != null)
        {
            enemyControl_.StopAllCoroutines();
        }

        DemoTimer.Instance.StopTimer();

        GameSceneManager.Instance.SetResultByServer(GameState.GameOver);
        GameSceneManager.Instance.ChangeStateByServer(nextState_);
    }
}
