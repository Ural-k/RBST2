using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerAttack : PlayerStatus
{
    [SerializeField] AttackParticles particles_;    //攻撃時のパーティクル
    [Header("クールダウン")]
    [SerializeField] float nowGCD_ = 0;             //グロバルCD
    [SerializeField] float nowCD1_ = 0;             //入力別クールダウン↓↓
    [SerializeField] float nowCD2_ = 0;
    [SerializeField] float nowCD3_ = 0;
    [SerializeField] int buffCount = 0;
    [SerializeField] int debuffCount = 0;

    enum SkillName
    {
        Fire,
        Meteor,
        Length,
    }
    [SerializeField] SkillName skill1_;
    [SerializeField] SkillName skill2_;
    [SerializeField] SkillName skill3_;
    delegate void Skills();
    Skills[] skills_ = new Skills[(int)SkillName.Length];
    //入力別スキル割り当て
    public void Attack1() { skills_[(int)skill1_](); }
    public void Attack2() { skills_[(int)skill2_](); }
    public void Attack3() { skills_[(int)skill3_](); }

    enum EffectName
    {
        Thunder,
        Length
    }
    EffectName effectName_;
    delegate EffectParameter Effects();
    Effects[] effects_ = new Effects[(int)EffectName.Length];

    List<Effects> nowEffects = new List<Effects>();//現在のバフ、デバフ

    protected void TakeMeBuff() { nowEffects.Add(effects_[(int)EffectName.Thunder]); }

    /// <summary>
    /// 初期化
    /// </summary>
    protected void InitalAttack()
    {
        //攻撃
        skills_[(int)SkillName.Fire] = Fire;                       //ファイア
        skills_[(int)SkillName.Meteor] = Meteor;                   //メテオ

        //バフ・デバフ
        effects_[(int)EffectName.Thunder] = Thunder;
    }


    /// <summary>
    /// プレイヤーの状態を更新
    /// </summary>
    protected IEnumerator PlayerStatusUpdate()
    {
        while (true)
        {
            //GCD・CD
            nowGCD_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);
            nowCD1_ = Mathf.Max(nowCD1_ - Time.deltaTime, 0);
            nowCD2_ = Mathf.Max(nowCD2_ - Time.deltaTime, 0);
            nowCD3_ = Mathf.Max(nowCD3_ - Time.deltaTime, 0);

            //バフ・デバフ
            foreach (var e in nowEffects)
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
                //e().interval_ -= Time.deltaTime;

            }

            yield return null;
        }
    }

    /*
     :  攻撃----------------------------------------------------
     */

    /// <summary>
    /// 単純な攻撃のテンプレ
    /// </summary>
    /// <param name="target">攻撃を発動させる対象</param>
    /// <param name="power">威力</param>
    /// <param name="radian">半径(0の場合矩形。縦横比も0の場合、単体攻撃)</param>
    /// <param name="wh">縦横比(0の場合円形。半径も0の場合、単体攻撃)</param>
    /// <param name="offset">中心座標</param>
    void OnAttack(Transform target, float power = 1, float radian = 0, Vector2? wh = null, Vector2? offset = null)
    {
        if (nowGCD_ != 0 || target == null) return;
        offset = offset ?? Vector2.zero;
    }

    /*
     :  スキル別関数↓↓---------------------------------------------
     */

    void Fire()
    {
        if (target_ == null || nowGCD_ != 0) return;
        string name = "ファイア";
        float gcd = 2.5f;
        int power = 5;
        nowGCD_ = gcd;  //GCD
        //範囲
        OnAttack(target_, power);
        Instantiate(particles_.fire_, target_.position, Quaternion.identity);
        //↓ここから特殊処理
        Debug.Log($"*{gameObject.name}*の{name}! → {power} ダメージ → *{target_.gameObject.name}*");

        //gcd,cd,delay
    }
    void Meteor()
    {

    }

    /*
     :  バフ・デバフ別関数↓↓----------------------------------------
     */

    /*
 :  バフ・デバフ----------------------------------------------
 */
    /// <summary>
    /// バフ・デバフ情報(正負でパラメータに効果を与える)
    /// </summary>
    protected struct EffectParameter
    {
        public string       name_;
        public float        lifeTime_;
        public float        interval_;
        public bool         debuff_;
        public bool         duplicate_;
        public Parameter    parameter_;
        public TargetType   targetType_;

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
    /*
     :  ---------------------------------------------------------
     */

    EffectParameter Thunder()
    {
        Parameter parameter = new();
        parameter.hp_ = -3.0f;
        EffectParameter effect = new();
        effect.SetParamater(
            parameter
            , TargetType.Player
            , "サンダー"
            , 10.0f
            , true
            , true
            , 3.0f
            );
        return effect;
    }
}