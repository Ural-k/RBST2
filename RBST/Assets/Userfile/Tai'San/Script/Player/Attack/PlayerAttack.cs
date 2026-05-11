using System.Collections;
using UnityEngine;

/// <summary>
/// 技倉庫
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    float nowGlobalCoolTime_ = 0;
    float nowCoolTime1_ = 0;
    float nowCoolTime2_ = 0;
    float nowCoolTime3_ = 0;

    /// <summary>
    /// シングルターゲットアタックテスト
    /// </summary>
    public void SingleTargetAttackTest(Player coller,Transform target, float power = 1,Vector2? scale = null, Vector2? offset = null)
    {
        if (nowGlobalCoolTime_ != 0 && target == null) return;
        offset = offset ?? Vector2.zero;
        scale = scale ?? Vector2.one;
        Debug.Log($"*{coller.gameObject.name}* → {power} ダメージ → *{target.gameObject.name}*");
    }

    /*
     :  コルーチンが必要な処理
     :  ・クールダウン
     :  ・グローバルクールダウン
     :  ・デバフ・バフ
     :  ・攻撃範囲の出るまでの時差etc
     */

    protected IEnumerator PlayerCoroutine()
    {
        while (true)
        {
            nowGlobalCoolTime_ = Mathf.Max(nowGlobalCoolTime_ - Time.deltaTime, 0);
            yield return null;
        }
    }

}
