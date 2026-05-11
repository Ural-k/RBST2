using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase, IDamageable
{
    void Update()
    {
        PlayerMove();
        if (Input.GetKeyUp(KeyCode.T)) OnTarget();
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SingleTargetAttackTest(this,target_, attack_, Vector2.one * 5);
    }
    
    public void TakeDamage(int damage_)
    {
        Debug.Log("当たっちゃったーワイプワイプ");
    }
}