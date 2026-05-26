using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : PlayerStatus
{
    [Header("攻撃の参照")]
    [SerializeField] AttackParticles particles_;
    [SerializeField] PlayerActionReference reference_;
    [SerializeField] List<Transform> demoEnemyList_;

    public void OnAction(Action name)
    {
        //---------------------------------------------------------------

        /*
         :<OnAction>
         :  ベースのアクション情報を取得
         :  自身のバフ・デバフを参照してアクション情報を変更
         :  範囲のコライダーを取得&パーティクル呼び出し
         :　         ↓
         :  (if)近くのターゲット取得
         :  ActionTypeごとの処理(攻撃)
         :
         :
         :
         :
         :
         :
         :
         :
         :
         :
         :
         */

        //ヒット結果

        //ベースのアクション情報を取得
        ActionInfo1 info = reference_.GetActionInfo(name);
        //自身のバフ・デバフを参照してアクション情報を変更
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        var ins = Instantiate(particles_.fire_, transform.position, Quaternion.identity);

        List<Transform> targetList = new List<Transform>();
        switch (info.targetType_)
        {
            case TargetType.Enemy:
                targetList = demoEnemyList_; 
                break;
            case TargetType.Player:
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
            case TargetType.Natural:
                targetList = demoEnemyList_;
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
        }

        /*
         :  ターゲット中心
         */
        Transform center = transform;
        if (info.toTarget_) center = targetList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();

        List<Transform> hitResult = GetHit(center, targetList);

        //List<Transform> GetHit(Transform center, List<Transform> list)
        //{
        //    List<Transform> hit = new List<Transform>();
        //    if (info.radius_ == 0 && info.aspect_ == Vector2.zero) { hit.Add(center); return hit; }
        //    hit.AddRange(list);
        //}



        //パーティクル大きさ調整
        if(info.radius_ != 0)//円形
        {
            ins.transform.position = resultTransform[0].position;
            ins.transform.localScale = Vector2.one * info.radius_;
            resultTransform[0].gameObject.GetComponent<DemoEnemyDamage>().GetComponent<IEnemyDamageAble>().DamageAble(info.power_);
        }

        ins.transform.position = (Vector2)resultTransform[0].position + info.offset_;

        //foreach (Collider2D hit in hits)
        //{
        //    if (hit.TryGetComponent<ActionDriver>(out _))
        //    {
        //        if (hit.GetComponent<ActionDriver>().targetType != TargetType.Enemy) return;
        //        Debug.Log($"あたった！。アクション名: {info.name_}");
        //    }        
    }
}

interface IEnemyDamageAble
{
    public void DamageAble(int damage);
}