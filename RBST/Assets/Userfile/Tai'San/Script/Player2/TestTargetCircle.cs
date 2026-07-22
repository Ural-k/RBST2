using UnityEngine;

interface ITestTargetCircle
{
    /// <summary>
    /// ターゲットサークルの半径の取得
    /// </summary>
    abstract float GetTargetRadius();
    /// <summary>
    /// ヒットポイントの引き算を実装(マイナスの場合回復)
    /// </summary>
    /// <param name="point">変動する値</param>
    abstract void SabHitPoint(int point);
}