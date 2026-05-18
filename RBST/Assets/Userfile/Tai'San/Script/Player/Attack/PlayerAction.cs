using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : PlayerStatus
{
    [SerializeField] AttackParticles particles_;
    [SerializeField] PlayerActionReference reference_;

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

        ActionInfo1 info = reference_.GetActionInfo(name);
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        position = (Vector2)transform.position + info.offset_;

        Collider2D[] hits = new Collider2D[0];
        bool circle = info.radius_ != 0;
        bool square = info.aspect_ != Vector2.zero;

        var ins = Instantiate(particles_.fire_, position, Quaternion.identity);//パーティクル
        if (circle)
        {
            ins.transform.localScale = Vector2.one * info.radius_;
            hits = Physics2D.OverlapCircleAll(position, info.radius_);
        }
        else if(square)
        {
            ins.transform.localScale = info.aspect_;
            hits = Physics2D.OverlapBoxAll(position, info.aspect_, 0);
        }
        
        if (hits.Length == 0) return;//当たっていない

        switch (info.target_)
        {
            case TargetType.Enemy:
                foreach(Collider2D hit in hits)
                {
                    if (hit.TryGetComponent<ActionDriver>(out _))
                    {
                        if (hit.GetComponent<ActionDriver>().targetType != TargetType.Enemy) return;
                        Debug.Log($"あたった！。アクション名: {info.name_}");
                    }
                }
                break;
            case TargetType.Player:
                foreach (Collider2D hit in hits)
                    if (hit.GetComponent<ActionDriver>().targetType != TargetType.Player) return;


                break;
            case TargetType.Natural:
                foreach (Collider2D hit in hits)
                    if (hit.GetComponent<ActionDriver>().targetType != TargetType.Enemy
                        && hit.GetComponent<ActionDriver>().targetType != TargetType.Player) return;


                break;
            case TargetType.Null:


                break;
        }
    }
}
