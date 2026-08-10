
public interface ITestTargetCircle
{
    abstract float CircleRadius { get; set; }
    abstract Effect Effect { get; set; }

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="point">変動する値</param>
    abstract void TakeDamage(float damage);

    /// <summary>
    /// 回復を与える
    /// </summary>
    /// <param name="point"></param>
    abstract void TakeHeal(float point);
}