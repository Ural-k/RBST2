
public interface ITestTargetCircle
{
    /// <summary>
    /// 中心座標の取得
    /// </summary>
    abstract UnityEngine.Vector2 GetPosition { get; }

    /// <summary>
    /// 判定の大きさ(半径)
    /// </summary>
    abstract float Radius { get; set; }

    /// <summary>
    /// バフ・デバフ関連
    /// </summary>
    abstract Effect Effect { get; set; }

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="damage">攻撃する値</param>
    /// <param name="from">与えた側</param>
    abstract void TakeDamage(int damage, ITestTargetCircle from = null);

    /// <summary>
    /// 回復する
    /// </summary>
    /// <param name="heal">回復させる値</param>
    /// <param name="from">与えた側</param>
    abstract void TakeHeal(int heal, ITestTargetCircle from = null);
}