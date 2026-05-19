using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : PlayerStatus
{
    [Header("攻撃の参照")]
    [SerializeField] AttackParticles particles_;
    [SerializeField] PlayerActionReference reference_;
    [SerializeField] List<Transform> demoEnemyList_;

    private void OnDrawGizmos()
    {
        if (target_ == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target_.position, 1);
    }

    public void OnAction(Action name)
    {
        //継承したときのみ使える変数(継承するか未定)
        Vector2 position;

        //---------------------------------------------------------------

        /*
         :<OnAction>
         :  ベースのアクション情報を取得
         :  自身のバフ・デバフを参照してアクション情報を変更
         :  範囲のコライダーを取得&パーティクル呼び出し
         :　         ↓
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
         :
         */

        //ベースのアクション情報を取得
        ActionInfo1 info = reference_.GetActionInfo(name);
        position = (Vector2)transform.position + info.offset_;//仮

        //自身のバフ・デバフを参照してアクション情報を変更
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        //コライダー取得&パーティクル呼び出し
        Collider2D[] hits = new Collider2D[0];
        bool circle = info.radius_ != 0;
        bool square = info.aspect_ != Vector2.zero;
        var ins = Instantiate(particles_.fire_, position, Quaternion.identity);
        {
            Collider2D[] tempHit = new Collider2D[0];
            if (circle)
            {
                ins.transform.localScale = Vector2.one * info.radius_;
                tempHit = Physics2D.OverlapCircleAll(position, info.radius_, 0);
            }
            else if (square)
            {
                ins.transform.localScale = info.aspect_;
                tempHit = Physics2D.OverlapBoxAll(position, info.aspect_, 0);
            }
        }

        if (hits.Length == 0) return;//当たっていない

        switch (info.actionType_)
        {
            case ActionType.ToEnemy:
                Transform target = GetNearTarget(demoEnemyList_);

                break;
            case ActionType.ToPlayer:
                break;
            case ActionType.OffsetToEnemy:
                break;
            case ActionType.OffsetToPlayer:
                break;
            case ActionType.JumpOnToEnemy:
                break;
            case ActionType.JumpOnToPlayer:
                break;

        }
        //foreach (Collider2D hit in hits)
        //{
        //    if (hit.TryGetComponent<ActionDriver>(out _))
        //    {
        //        if (hit.GetComponent<ActionDriver>().targetType != TargetType.Enemy) return;
        //        Debug.Log($"あたった！。アクション名: {info.name_}");
        //    }

        Transform GetNearTarget(List<Transform> list)
        {
            Vector2 myPos = gameObject.transform.position;
            Transform nearTransform = list.OrderBy(n => Mathf.Abs(Mathf.Abs(
                myPos.x - n.transform.position.x + (myPos.y - n.transform.position.y)
                ))).First();
            return nearTransform;
        }
        
        //コライダーEnemy・Playerの判別
    }

    /// <summary>
    /// ターゲット(左上から)
    /// </summary>
    public void OnTarget()      //将来的に近い敵からで取得したい
    {
        if (targetersObject_ == null || targetersObject_.transform.childCount == 0) return;

        //全ターゲット対象をList化
        List<Transform> unintentionalTargeter = new();
        for (int i = 0; i < targetersObject_.transform.childCount; ++i)
            unintentionalTargeter.Add(targetersObject_.transform.GetChild(i));

        //タゲ対象を左上優先で順番にList化
        var sortTargeter =
            unintentionalTargeter.OrderBy(n => n.position.x).ThenByDescending(n => n.position.y).ToList();

        //次項へターゲット
        if (target_ == null || target_ == sortTargeter[sortTargeter.Count - 1])
            target_ = sortTargeter.First();
        else
        {
            for (int i = 0; i < sortTargeter.Count - 1; ++i)
            {
                if (target_ == sortTargeter[i])
                {
                    target_ = sortTargeter[i + 1];
                    break;
                }
            }
        }

        //ターゲットUI操作
        if (targetGraphic_ == null) return;
        targetGraphic_.parent = target_;
        targetGraphic_.localPosition = Vector2.zero;
    }
}
