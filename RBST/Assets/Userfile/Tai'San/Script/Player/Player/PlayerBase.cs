using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : PlayerAttack, IDamageable
{
    private void Start()
    {
        PlayerManager.AddPlayer((Player)this);
        inputAxis_ = InputSystem.actions.FindAction("Move");
        InitalAttack();
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

    public void TakeDamage(int damage_)
    {
        Debug.Log("当たっちゃったーワイプワイプ");
    }
}