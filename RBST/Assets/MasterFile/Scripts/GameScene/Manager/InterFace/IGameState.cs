using UnityEngine;

public interface IGameState
{
    /// <summary>
    /// 起動処理
    /// </summary>
    public void Enter();

    /// <summary>
    /// メインループ
    /// </summary>
    public void Update();

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit();
}
