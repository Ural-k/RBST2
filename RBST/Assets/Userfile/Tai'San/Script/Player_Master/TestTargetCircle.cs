using UnityEngine;

public interface ITestTargetCircle
{
    /// <summary>
    /// ターゲットサークルの半径の取得
    /// </summary>
    abstract float GetTargetRadius();
    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="point">変動する値</param>
    abstract void TakeDamage(float damage);

    /// <summary>
    /// 回復処理
    /// </summary>
    /// <param name="point"></param>
    abstract void TakeHeal(float point);

    /// <summary>
    /// バフ・デバフ
    /// </summary>
    /// <param name="data"></param>
    abstract void AddEffect(EffectData data);
}