using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    void Update()
    {
        PlayerMove();
        if (Input.GetKeyUp(KeyCode.T)) OnTarget();
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Attack1();
        //SingleTargetAttackTest(this, target_, attack_);
    }
}