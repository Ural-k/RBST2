using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

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
        //継承したときのみ使える変数
        Vector2 position;

        //---------------------------------------------------------------

        ActionInfo1 info = reference_.GetActionInfo(name);
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        position = (Vector2)transform.position + info.offset_;
        Instantiate(particles_.fire_, position, Quaternion.identity);//パーティクル

        List<Collider2D> hits = new List<Collider2D>();
        Collider2D[] circleHit = Physics2D.OverlapCircleAll(position, info.radius_);
        //Collider2D[] squareHit = Physics2D.OverlapBoxAll(position, info.aspect_, 0);//angle仮
        foreach(Collider2D c in circleHit) { hits.Add(c); }
        //foreach(Collider2D c in squareHit) { hits.Add(c); }
        
        if (hits.Count == 0) return;

        bool square = info.aspect_ != Vector2.zero;
        bool circle = info.radius_ != 0;
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
