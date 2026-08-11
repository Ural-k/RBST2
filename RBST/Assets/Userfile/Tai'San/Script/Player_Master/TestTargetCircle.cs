
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
    /// <param name="point">変動する値</param>
    abstract void TakeDamage(int damage);

    /// <summary>
    /// 回復を与える
    /// </summary>
    /// <param name="point"></param>
    abstract void TakeHeal(int point);
}