using Unity.Netcode;
using UnityEngine;

/// <summary>
/// その他機能
/// </summary>
public class PlayerBase : PlayerSkill, IDamageable, IToEnemyDamageAble//Avatar
{
    void Awake()
    {
        PlayerManager.AddPlayer((Player)this);
        ParticleManager.InstanceLoad();
    }

    private void Start()
    {
        info_.effect_.GetAllBuff();
        info_.Initialize();
        StartCoroutine(CoolTimeCoroutine());
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

        if (transform.position != result)
        {
            animator_.SetTrigger(IS_MOVING_HASH);
        }
        else 
        {
            animator_.ResetTrigger(IS_MOVING_HASH);
        }

        transform.position = result;
    }

    public void TakeDamage(int damage)
    {
        info_.parameter_.HP -= damage;
        ShowFloatingText(damage, FloatingTextType.PlayerDamage);
        if (info_.parameter_.HP == 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    public void DamageAble(int damage)
    {
        info_.parameter_.HP -= damage;
        if (info_.parameter_.HP == 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }

    void Death()
    {

    }
}