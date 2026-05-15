using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : PlayerStatus
{
    private enum EffectName
    {
        Thunder,
        Length
    }
    [SerializeField] 
    private EffectName effectName_;
    private EffectBase[] effects_ = new EffectBase[(int)EffectName.Length];
    private List<EffectBase> nowEffects_ = new List<EffectBase>();//現在のバフ、デバフ

    protected void TakeMeBuff() { nowEffects_.Add(effects_[(int)effectName_]); }


    private class EffectBase/* : IEffect*/
    {
        public Parameter parameter_ = new();
        public EffectInfo info_ = new();
        public extern virtual Parameter GetParameter();
        public extern virtual EffectInfo GetInfo();
        public extern virtual void EndAction();
    }

    private class Thunder : EffectBase/*,IEffect*/
    {
        public Thunder()
        {
            parameter_.hp_ = -3.0f;  //効果内容
            info_.SetParamater(    //デバフ情報
              parameter_
            , TargetType.Player
            , "サンダー"
            , 10.0f
            , true
            , true
            , 3.0f
            );
            Debug.Log("初期化");
        }
        public override Parameter GetParameter() { return parameter_; }
        public override EffectInfo GetInfo() { return info_; }
        public override void EndAction() { Debug.Log("終了"); }
    }

    /*
 :  バフ・デバフ----------------------------------------------
 */
    /// <summary>
    /// バフ・デバフ情報(正負でパラメータに効果を与える)
    /// </summary>
    protected struct EffectInfo
    {
        public string name_;
        public float lifeTime_;
        public float interval_;
        public bool debuff_;
        public bool duplicate_;
        public Parameter parameter_;
        public TargetType targetType_;
        public float nowInterval;

        /// <summary>
        /// バフ・デバフのパラメーター一括セット
        /// </summary>
        /// <param name="parameter">変動するパラメーター</param>
        /// <param name="targetType">対象範囲</param>
        /// <param name="name">名前</param>
        /// <param name="lifeTime">付与時間</param>
        /// <param name="debuff">デバフかどうか</param>
        /// <param name="duplicate">重複するかどうか</param>
        /// <param name="interval">効果が発生する間隔(継続ダメージなどに使う)</param>
        public void SetParamater(
              Parameter parameter
            , TargetType targetType
            , string name
            , float lifeTime
            , bool debuff
            , bool duplicate = true
            , float interval = 0)
        {
            name_ = name;
            lifeTime_ = lifeTime;
            interval_ = interval;
            debuff_ = debuff;
            duplicate_ = duplicate;
            parameter_ = parameter;
        }
    }

    protected void EffectInit()
    {
        //バフ・デバフ
        effects_[(int)EffectName.Thunder] = new Thunder();
    }

    protected void EffectUpdate()
    {
        //バフ・デバフ
        foreach (var e in nowEffects_)
        {
            /*
             :name
             :付与時間in
             :間隔lif
             :デバフdeb
             :重複dup
             :targetType
             :parameter
             */
            if (e.info_.lifeTime_ >= 0)//ライフタイム
            {
                e.info_.lifeTime_ -= Time.deltaTime;
                if (e.info_.nowInterval != 0)
                {
                    e.info_.nowInterval = Mathf.Max(e.info_.nowInterval - Time.deltaTime, 0);
                }
                else
                {
                    SubtractionParameter(e.parameter_);
                    e.info_.nowInterval = e.info_.interval_;
                }
            }
            else
            {
                e.EndAction();
                nowEffects_.Remove(e);
            }
        }
    }

}
