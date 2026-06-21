using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

/*
 :  ・デバフを与える　activeDebuffs |= debuff;
 :  ・デバフを解除　　activeDebuffs &= ~debuff;
 :  ・デバフが有効か　return (activeDebuffs & debuff) != 0;
 :  ・複数デバフ確認（どれか1つでもあればtrue）　return (activeDebuffs & debuffs) != 0;
 :  ・複数のデバフをすべて持っているか確認　　　　return (activeDebuffs & debuffs) == debuffs;
 :  ・全デバフ解除　　activeDebuffs = Debuff.None;
 :
 :  ・付与例（複数可）AddDebuff(Debuff.Poison | Debuff.Slow);
 */

public struct Effect
{
    private TEffect effect_;                        //全ての効果
    private Dictionary<TEffect, float> effectTime_; //時間
    private Dictionary<TEffect, float> effectPower_;// デバフの強度を管理（例: スローなら0.5 = 速度50%減

    /// <summary>
    /// 効果を付与する
    /// </summary>
    /// <param name="effect">Buff or DeBuffを代入</param>
    public void Add(TEffect effect, EffectInfo info)
    {
        if (/*effectPower_[effect] <= info.power_*/true)
        {
            effect_ = effect;
            effectTime_[effect] = info.time_;
            effectPower_[effect] = info.power_;
        }
    }

    public void DebugAllEffect()
    {
        string result = "";
        //foreach(TEffect effect in effectTime_)
        //{
        //    if (effect is BuffEffect b) {  }
        //}
    }

    /// <summary>
    /// 全ての効果時間をTime.deltaTime引く
    /// </summary>
    public void AllEffectTimer() { if(effectTime_ != null) foreach (TEffect e in effectTime_.Keys) { effectTime_[e] -= Time.deltaTime; } }

    //// どのデバフが有効かをビットフラグで管理
    //private TEffect activeDebuffFlags;
}