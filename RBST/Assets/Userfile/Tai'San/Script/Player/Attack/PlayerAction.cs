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
        List<Transform> resultTransform = new List<Transform>();

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

        //ここ汚いからのちのち変える
        bool typeCircle = false;
        bool typeSquare = false;
        bool typeSingle = false;
        bool targetEnemy = false;
        bool targetPlayer = false;

        if(info.radius_ != 0) typeCircle = true;
        else if(info.aspect_ != Vector2.zero) typeSquare = true;
        else typeSingle = true;
        if(info.targetType_ == TargetType.Enemy) targetEnemy = true;
        else if(info.targetType_ == TargetType.Player) targetPlayer = true;

        //近いターゲット取得
        if (info.toTarget_)
        {
            if (targetEnemy)
                resultTransform.Add(demoEnemyList_.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First());
            else if (targetPlayer)
            {
                List<Transform> playerList = new List<Transform>();
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) playerList.Add(PlayerManager.GetPlayer(i).transform);
                resultTransform.Add(playerList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First());
            }
        }
        else resultTransform.Add(transform);

        //パーティクル大きさ調整
        if(typeCircle)//円形
        {
            ins.transform.position = resultTransform[0].position;
            ins.transform.localScale = Vector2.one * info.radius_;
            //if (targetEnemy)
            //{
            //    resultTransform.Add(
            //        (Transform)demoEnemyList_.OrderBy
            //        (n => Vector2.Distance(resultTransform[0].transform.position, n.transform.position) < info.radius_));
            //}
            //else if(targetPlayer)
            //{
            //    List<Transform> playerList = new List<Transform>();
            //    for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) playerList.Add(PlayerManager.GetPlayer(i).transform);
            //    resultTransform.Add((Transform)playerList.Where(n => Vector2.Distance(transform.position, n.transform.position)));
            //}
            resultTransform[0].gameObject.GetComponent<DemoEnemyDamage>().GetComponent<IEnemyDamageAble>().DamageAble(info.power_);
        }
        else if(typeSquare)//矩形
        {
            ins.transform.localScale = info.aspect_;
        }
        else//単体
        {
            typeSingle = true;
        }

        ins.transform.position = (Vector2)resultTransform[0].position + info.offset_;

        if (hits.Length == 0) return;//当たっていない



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