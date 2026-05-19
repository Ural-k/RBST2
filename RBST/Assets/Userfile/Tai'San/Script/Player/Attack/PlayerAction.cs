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
        //継承したときのみ使える変数(継承するか未定)
        Vector2 position = (Vector2)transform.position;
        Transform resultTransform = null;

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

        //ベースのアクション情報を取得
        ActionInfo1 info = reference_.GetActionInfo(name);

        //自身のバフ・デバフを参照してアクション情報を変更
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        //コライダー取得&パーティクル呼び出し
        Collider2D[] hits = new Collider2D[0];
        var ins = Instantiate(particles_.fire_, position, Quaternion.identity);

        //近いターゲット取得
        if(info.toTarget_ && info.targetType_ == TargetType.Enemy)
            resultTransform = demoEnemyList_.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();
        else if(info.toTarget_ && info.targetType_ == TargetType.Player)
        {
            List<Transform> playerList = new List<Transform>();
            for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) playerList.Add(PlayerManager.GetPlayer(i).transform);
            resultTransform = playerList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();
        }

        bool singleAttack = false;

        //パーティクル大きさ調整
        if(info.radius_ != 0)//円形
        {
            ins.transform.localScale = Vector2.one * info.radius_;
            //resultTransform.GetComponent<>
        }
        else if(info.aspect_ != Vector2.zero)//矩形
        {
            ins.transform.localScale = info.aspect_;
        }
        else//単体
        {
            singleAttack = true;
        }

        ins.transform.position = (Vector2)resultTransform.position + info.offset_;

        if (hits.Length == 0) return;//当たっていない



        //foreach (Collider2D hit in hits)
        //{
        //    if (hit.TryGetComponent<ActionDriver>(out _))
        //    {
        //        if (hit.GetComponent<ActionDriver>().targetType != TargetType.Enemy) return;
        //        Debug.Log($"あたった！。アクション名: {info.name_}");
        //    }
        
        void TestAttack()
        {

        }
    }

    /// <summary>
    /// ターゲット(左上から)
    /// </summary>
    public void OnTarget()      //将来的に近い敵からで取得したい
    {
        //if (targetersObject_ == null || targetersObject_.transform.childCount == 0) return;

        ////全ターゲット対象をList化
        //List<Transform> unintentionalTargeter = new();
        //for (int i = 0; i < targetersObject_.transform.childCount; ++i)
        //    unintentionalTargeter.Add(targetersObject_.transform.GetChild(i));

        ////タゲ対象を左上優先で順番にList化
        //var sortTargeter =
        //    unintentionalTargeter.OrderBy(n => n.position.x).ThenByDescending(n => n.position.y).ToList();

        ////次項へターゲット
        //if (target_ == null || target_ == sortTargeter[sortTargeter.Count - 1])
        //    target_ = sortTargeter.First();
        //else
        //{
        //    for (int i = 0; i < sortTargeter.Count - 1; ++i)
        //    {
        //        if (target_ == sortTargeter[i])
        //        {
        //            target_ = sortTargeter[i + 1];
        //            break;
        //        }
        //    }
        //}

        ////ターゲットUI操作
        //if (targetGraphic_ == null) return;
        //targetGraphic_.parent = target_;
        //targetGraphic_.localPosition = Vector2.zero;
    }
}

interface IEnemyDamageAble
{
    public void DamageAble();
}