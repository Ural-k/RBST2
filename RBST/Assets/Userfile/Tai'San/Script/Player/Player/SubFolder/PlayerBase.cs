using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// その他機能
/// </summary>
public class PlayerBase : PlayerSkill, IDamageable,IToEnemyDamageAble
{
    private void Start()
    {
        info_.inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(CoolTimeCoroutine());
        info_.skillData_ = JobData.GetJobSkill(info_.parameter_.jobNumber_);
        info_.filip_ = 1;
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {
        Vector2 move_value = info_.inputAxis_.ReadValue<Vector2>();
        info_.LookAt(move_value + (Vector2)transform.position, transform);
        move_value *= info_.parameter_.speed_ * Time.deltaTime;
        Vector3 result = new Vector2(
                Mathf.Clamp(transform.position.x + move_value.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
                Mathf.Clamp(transform.position.y + move_value.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
            );
        transform.position = result;
    }

    public void TakeDamage(int damage)
    {
        info_.parameter_.hp_ -= damage;
        if(info_.parameter_.hp_ <= 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    public void DamageAble(int damage)
    {
        info_.parameter_.hp_ -= damage;
        if (info_.parameter_.hp_ <= 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    void Death()
    {

    }
}