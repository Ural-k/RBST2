using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] AttackParticles particles_;
    [SerializeField] PlayerActionReference reference_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) OnAction(Action.Fire);
    }

    public void OnAction(Action name)
    {
        ActionInfo1 info = reference_.GetActionInfo(name);
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        Vector2 pos = Vector2.zero; //仮座標

        Instantiate(particles_.fire_, pos, Quaternion.identity);//仮particle

        List<Collider2D> hits = new List<Collider2D>();
        Collider2D[] circleHit = Physics2D.OverlapCircleAll(pos, info.radius_);
        Collider2D[] squareHit = Physics2D.OverlapBoxAll(pos, info.aspect_, 0);//position,angle仮
        foreach(Collider2D c in circleHit) { hits.Add(c); }
        foreach(Collider2D c in squareHit) { hits.Add(c); }
        
        if (hits.Count == 0) return;

        bool square = info.aspect_ != Vector2.zero;
        bool circle = info.radius_ != 0;
        switch (info.type_)
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
