using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : PlayerAction, IDamageable
{
    private void Start()
    {
        PlayerManager.AddPlayer((Player)this);
        inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(ActionCoroutine());
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