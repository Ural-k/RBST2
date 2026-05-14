using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    void Update()
    {
        PlayerMove();
        if (Input.GetKeyUp(KeyCode.Space)) OnTarget();  //デバック用仮処理↓↓
        if (Input.GetMouseButtonDown(1)) Attack2();
    }

    public void InputAttack1(InputAction.CallbackContext context) { if (context.performed) Attack1(); }
    public void InputAttack2(InputAction.CallbackContext context) { if (context.performed) Attack2(); }
    public void InputAttack3(InputAction.CallbackContext context) { if (context.performed) Attack3(); }
}