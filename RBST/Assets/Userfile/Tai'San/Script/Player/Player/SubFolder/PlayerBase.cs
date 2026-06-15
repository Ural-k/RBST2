using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// その他機能
/// </summary>
public class PlayerBase : PlayerSkill, IDamageable
{
    private void Start()
    {
        info_.inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(CoolTimeCoroutine());
        info_.skillData_ = JobData.GetJobSkill(info_.parameter_.jobNumber_);
        info_.filip_ = 1;
        PlayerManager.AddPlayer((Player)this);//仮
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {
        Vector2 move_value = info_.inputAxis_.ReadValue<Vector2>();
        info_.LookAt(move_value + (Vector2)transform.position, transform);
        move_value *= info_.parameter_.speed_ * Time.deltaTime;
        transform.position += (Vector3)move_value;
    }

    public void TakeDamage(int damage_)
    {
        info_.parameter_.hp_ -= damage_;
        if(info_.parameter_.hp_ <= 0)
        {
            Debug.Log("死んだ！");
            info_.downTime_ = 5f;//マジック
        }
    }
}