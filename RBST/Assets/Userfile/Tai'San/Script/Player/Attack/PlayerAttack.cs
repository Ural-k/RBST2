using System.Collections;
using UnityEngine;

/// <summary>
/// 技倉庫
/// </summary>
public class PlayerAttack : PlayerStatus
{
    [SerializeField] AttackEffectReference effects_;
    [Header("クールダウン")]
    [SerializeField] float nowGCD_ = 0;
    [SerializeField] float nowCD1_ = 0;
    [SerializeField] float nowCD2_ = 0;
    [SerializeField] float nowCD3_ = 0;

    IEnumerator PlayerStatusUpdate()
    {
        while (true)
        {
            //GCD・CD
            nowGCD_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);
            nowCD1_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);
            nowCD2_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);
            nowCD3_ = Mathf.Max(nowGCD_ - Time.deltaTime, 0);

            //バフ・デバフ


            yield return null;
        }
    }

    enum Skill
    {
        Fire,
        Meteor,
        Length,
    }
    [SerializeField] Skill skill1_;
    [SerializeField] Skill skill2_;
    [SerializeField] Skill skill3_;
    delegate void Attacks();
    Attacks[] skills_ = new Attacks[(int)Skill.Length];

    public void Attack1() { skills_[(int)Skill.Fire](); }
    public void Attack2() { skills_[(int)skill2_](); }
    public void Attack3() { skills_[(int)skill3_](); }

    void OnAttack(Transform target, float power = 1, float radian = 0, Vector2? wh = null, Vector2? scale = null, Vector2? offset = null)
    {
        if (nowGCD_ != 0 || target == null) return;
        offset = offset ?? Vector2.zero;
        scale = scale ?? Vector2.one;
    }

    protected void InitalAttack()
    {
        skills_[(int)Skill.Fire] = Fire;                       //ファイア
        skills_[(int)Skill.Meteor] = Meteor;                   //メテオ
    }

    /*
     :  スキル↓↓
     */

    public void Fire()
    {
        if (target_ == null || nowGCD_ != 0) return;
        string name = "ファイア";
        float gcd = 2.5f;
        int power = 5;
        nowGCD_ = gcd;  //GCD
        //範囲
        OnAttack(target_, power);
        Instantiate(effects_.fire_, target_.position, Quaternion.identity);
        //↓ここから特殊処理
        Debug.Log($"*{gameObject.name}*の{name}! → {power} ダメージ → *{target_.gameObject.name}*");

        //gcd,cd,delay
    }
    void Meteor()
    {

    }
}