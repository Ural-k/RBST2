using UnityEngine;
using System.Collections.Generic;

public interface ITestTargetCircle
{
    /// <summary>
    /// ターゲットサークルの半径の取得
    /// </summary>
    abstract float GetTargetRadius();
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

    /// <summary>
    /// バフ・デバフ付与
    /// </summary>
    /// <param name="data"></param>
    /// <param name="time">付与時間</param>
    abstract void AddEffect(EffectData data, int time);

    /// <summary>
    /// バフ・デバフ解除
    /// </summary>
    /// <param name="data">解除する効果</param>
    abstract void RemoveEffect(EffectData data);

    /// <summary>
    /// バフ・デバフ解除
    /// </summary>
    /// <param name="num">解除数(残り秒数の多いデバフから解除)</param>
    abstract void RemoveEffect(int num = 1);
}