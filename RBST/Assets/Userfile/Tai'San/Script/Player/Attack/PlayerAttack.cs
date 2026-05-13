using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技倉庫
/// </summary>
public class PlayerAttack : PlayerStatus
{
    [SerializeField] AttackEffectReference effect_;
    [Header("クールダウン")]
    [SerializeField] float nowGlobalCoolTime_ = 0;
    [SerializeField] float nowCoolTime1_ = 0;
    [SerializeField] float nowCoolTime2_ = 0;
    [SerializeField] float nowCoolTime3_ = 0;

    IEnumerator GlobalCoolDown(float gcd)
    {
        nowGlobalCoolTime_ = gcd;
        while (nowGlobalCoolTime_ != 0)
        {
            nowGlobalCoolTime_ = Mathf.Max(nowGlobalCoolTime_ - Time.deltaTime, 0);
            yield return null;
        }
    }
    IEnumerator CoolDown1(float cd)
    {
        nowCoolTime1_ = cd;
        while (nowCoolTime1_ != 0)
        {
            nowCoolTime1_ = Mathf.Max(nowCoolTime1_ - Time.deltaTime, 0);
            yield return null;
        }
    }
    IEnumerator CoolDown2(float cd)
    {
        nowCoolTime2_ = cd;
        while (nowCoolTime1_ != 0)
        {
            nowCoolTime2_ = Mathf.Max(nowCoolTime2_ - Time.deltaTime, 0);
            yield return null;
        }
    }
    IEnumerator CoolDown3(float cd)
    {
        nowCoolTime3_ = cd;
        while (nowCoolTime1_ != 0)
        {
            nowCoolTime3_ = Mathf.Max(nowCoolTime3_ - Time.deltaTime, 0);
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
    Attacks[] attacks_ = new Attacks[(int)Skill.Length];

    public void Attack1() { attacks_[(int)Skill.Fire](); }
    public void Attack2() { attacks_[(int)skill2_](); }
    public void Attack3() { attacks_[(int)skill3_](); }

    void OnAttack(Transform target, float power = 1, float radian = 0, Vector2? wh = null, Vector2? scale = null, Vector2? offset = null)
    {
        if (nowGlobalCoolTime_ != 0 || target == null) return;
        offset = offset ?? Vector2.zero;
        scale = scale ?? Vector2.one;
    }

    protected void InitalAttack()
    {
        attacks_[(int)Skill.Fire] = Fire;                       //ファイア
        attacks_[(int)Skill.Meteor] = Meteor;                   //メテオ
    }

    /*
     :  スキル↓↓
     */

    public void Fire()
    {
        if (target_ == null || nowGlobalCoolTime_ != 0) return;
        string name = "ファイア";
        float gcd = 2.5f;
        int power = 5;
        StartCoroutine(GlobalCoolDown(gcd));  //GCD
        //範囲
        OnAttack(target_, power);
        Instantiate(effect_.fire_, target_.position, Quaternion.identity);
        //↓ここから特殊処理
        Debug.Log($"*{gameObject.name}*の{name}! → {power} ダメージ → *{target_.gameObject.name}*");

        //gcd,cd,delay
    }
    void Meteor()
    {

    }
}