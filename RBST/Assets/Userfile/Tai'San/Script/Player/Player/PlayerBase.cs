using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : PlayerAttack
{
    [Header("ステータス")]
    [SerializeField] protected string   jobName_;                  //職業名
    [SerializeField] protected string   playerName_;               //プレイヤーの名前
    [SerializeField] protected float    speed_;                    //移動速度
    [SerializeField] protected float    attack_;                   //攻撃力
    [SerializeField] protected float    defense_;                  //防御力
    [SerializeField] protected float    gcd_;                      //グローバルクールダウン
    [SerializeField] protected float    critical_;                 //クリティカル率
    [SerializeField] protected float    lv_;

    protected InputAction           inputAxis_;                //移動キー入力
    protected Transform             target_;                   //ターゲット中のTransfrom

    [Header("ターゲット")]
    [SerializeField] GameObject     targetersObject_;          //ターゲット可能なオブジェクト群の親
    [SerializeField] Transform      targetGraphic_;            //ターゲット表示

    private void Start()
    {
        PlayerManager.AddPlayer((Player)this);
        inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(PlayerCoroutine());
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {
        Vector2 move_value = inputAxis_.ReadValue<Vector2>();
        move_value *= speed_ * Time.deltaTime;
        transform.position += (Vector3)move_value;
    }

    /// <summary>
    /// ターゲット(左上から)
    /// </summary>
    protected void OnTarget()
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