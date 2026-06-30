using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int buff_;
    private int debuff_;
    private Dictionary<TEffect, float> effectTime_; //時間
    private Dictionary<TEffect, float> effectPower_;// デバフの強度を管理（例: スローなら0.5 = 速度50%減

    /// <summary>
    /// 効果を付与する
    /// </summary>
    /// <param name="effect">Buff or DeBuffを代入</param>
    public void Add(TEffect effect/*, EffectInfo info*/)
    {
        if (/*effectPower_[effect] <= info.power_*/true)
        {
            if (effect is BuffEffect b) { /*buff_ |= (int)b.Value; Debug.Log("buff");*/ }
            else if (effect is DebuffEffect d) { /*debuff_ |= (int)d.Value; Debug.Log("debuff");*/ }

            //effectTime_[effect] = info.time_;
            //effectPower_[effect] = info.power_;
        }
    }

    /// <summary>
    /// 全てのバフを取得
    /// </summary>
    public void GetAllBuff()
    {
        //Debug.Log(buff_);
        //Debug.Log("memo: " + Enum.GetValues(typeof(Debuff)));
        foreach (Debuff d in Enum.GetValues(typeof(Debuff)))
        {
            if (d == Debuff.None) continue;
            if ((buff_ & (int)d) != 0) Debug.Log(d);
        }
        //static bool Has(this int debuffs, Debuff d) => (debuffs & (int)d) != 0;
    }

    /// <summary>
    /// 全ての効果時間をTime.deltaTime引く
    /// </summary>
    public void AllEffectTimer() { if(effectTime_ != null) foreach (TEffect e in effectTime_.Keys) { effectTime_[e] -= Time.deltaTime; } }

    public bool CheckEffect(TEffect effect)
    {
        bool result = false;
        if      (effect is BuffEffect b)    result = (buff_ & (int)b.Value) != 0;
        else if (effect is DebuffEffect d)  result = (buff_ & (int)d.Value) != 0;
        return result;
    }

    public void AllClearEffect() { buff_ = (int)Buff.None; debuff_ = (int)Debuff.None; }



    //// どのデバフが有効かをビットフラグで管理
    //private TEffect activeDebuffFlags;
}