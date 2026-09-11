using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// エフェクト機能と変数。(イベントにつきローカル)
/// </summary>
public class Effect : MonoBehaviour
{
    public Effect(ITargetCircle targetCircle) => target_ = targetCircle;

    private List<EffectController> active_ = new();
    private float tickTimer_;
    private ITargetCircle target_;

    public event Action<EffectController> OnEffectAdd;
    public event Action<EffectController> OnEffectRemove;
    public List<EffectController> GetActive { get { return active_; } }

    /// <summary>
    /// バフ・デバフを付与する
    /// </summary>
    /// <param name="data">効果</param>
    /// <param name="time">付与する秒数</param>
    public void AddEffect(EffectData data, int time)
    {
        var effect = active_.Find(n => n.data_.id_ == data.id_);
        if (effect != null && data.maxStack_ > 1)
        {
            effect.stackCount_ = Mathf.Min(effect.stackCount_ + 1, data.maxStack_);
            effect.timer_ = time;
            return;
        }

        var instance = new EffectController { data_ = data, timer_ = time, target_ = target_ };
        active_.Add(instance);
        foreach (var e in data.effects_) e.OnApply(instance);
        OnEffectAdd?.Invoke(instance);
    }

    /// <summary>
    /// バフ・デバフを指定して解除する
    /// </summary>
    /// <param name="data">解除する効果</param>
    public void RemoveEffect(EffectData data)
    {
        var effect = active_.FirstOrDefault(n => n.data_.id_ == data.id_);
        if (effect == null) { return; }

        foreach (var e in effect.data_.effects_) e.OnRemove(effect);
        OnEffectRemove?.Invoke(effect);
        active_.Remove(effect);
    }

    /// <summary>
    /// デバフを残り秒数が多い順に解除する
    /// </summary>
    /// <param name="num">解除する効果の数</param>
    public void RemoveEffect(int num)
    {
        for (int i = 0; i < num; ++i)
        {
            var debuf = active_.Where(n => n.data_.type_ == EffectType.Debuff).ToList();  //デバフのみ
            if (debuf.Count == 0) return;

            var effect = debuf?.OrderByDescending(n => n.timer_).First();                        //秒数の多い方から解除
            foreach (var e in effect.data_.effects_) e.OnRemove(effect);
            OnEffectRemove?.Invoke(effect);
            active_.Remove(effect);
        }
    }

    /// <summary>
    /// バフ・デバフの１秒単位で行われる処理(Updateに入れておけばOK)
    /// </summary>
    public void TickEffect()
    {
        tickTimer_ += Time.deltaTime;
        if (tickTimer_ < 1f) return;
        tickTimer_ -= 1f;

        for (int i = active_.Count - 1; i >= 0; --i)
        {
            var effect = active_[i];
            effect.timer_ -= 1f;
            foreach (var e in effect.data_.effects_) e.OnTick(effect);

            if (effect.timer_ <= 0)
            {
                foreach (var e in effect.data_.effects_) e.OnRemove(effect);
                OnEffectRemove?.Invoke(effect);
                active_.RemoveAt(i);
            }
        }
    }

}
