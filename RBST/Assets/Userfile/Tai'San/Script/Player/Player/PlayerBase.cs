using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// その他機能
/// </summary>
public class PlayerBase : PlayerSkill, IDamageable
{
    private void Start()
    {
        PlayerManager.AddPlayer((Player)this);
        inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(CoolTimeCoroutine());
        skillData_ = JobData.GetJobSkill(parameter_.jobNumber_);
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {
        Vector2 move_value = inputAxis_.ReadValue<Vector2>();
        move_value *= parameter_.speed_ * Time.deltaTime;
        transform.position += (Vector3)move_value;
    }

    public void TakeDamage(int damage_)
    {
        Debug.Log("当たっちゃったーワイプワイプ");
    }
}