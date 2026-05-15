using System.Collections;
using UnityEngine;

public class PlayerAttack : PlayerEffect
{
    [SerializeField] private AttackParticles particles_;    //攻撃時のパーティクル
    [Header("クールダウン")]
    [SerializeField] private float nowGCD_ = 0;             //グロバルCD
    [SerializeField] private float nowCD1_ = 0;             //入力別クールダウン↓↓
    [SerializeField] private float nowCD2_ = 0;
    [SerializeField] private float nowCD3_ = 0;
    [SerializeField] private int buffCount = 0;
    [SerializeField] private int debuffCount = 0;

    private enum SkillName
    {
        Fire,
        Meteor,
        Length,
    }
    [SerializeField] private SkillName skill1_;
    [SerializeField] private SkillName skill2_;
    [SerializeField] private SkillName skill3_;
    private delegate void Skills();
    private Skills[] skills_ = new Skills[(int)SkillName.Length];
    //入力別スキル割り当て
    public void Attack1() { skills_[(int)skill1_](); }
    public void Attack2() { skills_[(int)skill2_](); }
    public void Attack3() { skills_[(int)skill3_](); }

    /// <summary>
    /// 初期化
    /// </summary>
    private protected void InitalAttack()
    {
        //攻撃
        skills_[(int)SkillName.Fire] = Fire;                       //ファイア
        skills_[(int)SkillName.Meteor] = Meteor;                   //メテオ

        EffectInit();
    }

    /// <summary>
    /// プレイヤーの状態を更新
    /// </summary>
    private protected IEnumerator PlayerStatusUpdate()
    {
        while (true)
        {
            //GCD・CD
            nowGCD_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);
            nowCD1_ = Mathf.Max(nowCD1_ - Time.deltaTime, 0);
            nowCD2_ = Mathf.Max(nowCD2_ - Time.deltaTime, 0);
            nowCD3_ = Mathf.Max(nowCD3_ - Time.deltaTime, 0);

            EffectUpdate();

            yield return null;
        }
    }

    /// <summary>
    /// 単純な攻撃のテンプレ
    /// </summary>
    /// <param name="target">攻撃を発動させる対象</param>
    /// <param name="power">威力</param>
    /// <param name="radian">半径(0の場合矩形。縦横比も0の場合、単体攻撃)</param>
    /// <param name="wh">縦横比(0の場合円形。半径も0の場合、単体攻撃)</param>
    /// <param name="offset">中心座標</param>
    private void OnAttack(Transform target, float power = 1, float radian = 0, Vector2? wh = null, Vector2? offset = null)
    {
        if (nowGCD_ != 0 || target == null) return;
        offset = offset ?? Vector2.zero;
    }

    /*
     :  スキル別関数↓↓---------------------------------------------
     */

    private void Fire()
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
    private void Meteor()
    {

    }
}