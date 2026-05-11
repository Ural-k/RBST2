using System.Collections;
using UnityEngine;

/// <summary>
/// 技倉庫
/// </summary>
public class PlayerAttack : PlayerStatus,IPlayerAttack
{
    /*
     :  コルーチンが必要な処理
     :  ・クールダウン
     :  ・グローバルクールダウン
     :  ・デバフ・バフ
     :  ・攻撃範囲の出るまでの時差etc
     */

    [Header("クールダウン")]
    [SerializeField] float nowGlobalCoolTime_ = 0;
    [SerializeField] float nowCoolTime1_ = 0;
    [SerializeField] float nowCoolTime2_ = 0;
    [SerializeField] float nowCoolTime3_ = 0;

    /*
     :  クールダウン
     */
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

    PlayerAttackData fire_ = new PlayerAttackData(
        "ファイア",
        50,
        3,
        2,
        4,
        0,
        0,
        0
        );
    


    public void Attack1() { Debug.Log("攻撃1"); }
    public void Attack2() { Debug.Log("攻撃2"); }
    public void Attack3() { Debug.Log("攻撃3"); }

    /// <summary>
    /// シングルターゲットアタックテスト
    /// </summary>
    public void SingleTargetAttackTest(Player coller,Transform target, float power = 1,Vector2? scale = null, Vector2? offset = null)
    {
        if (nowGlobalCoolTime_ != 0 || target == null) return;
        StartCoroutine(GlobalCoolDown(5));
        /*
         :  登録する情報
         :  ・gcd
         :  ・cd
         :  ・delay
         :  ・
         :
         */
        offset = offset ?? Vector2.zero;
        scale = scale ?? Vector2.one;
        Debug.Log($"*{coller.gameObject.name}* → {power} ダメージ → *{target.gameObject.name}*");
    }



}