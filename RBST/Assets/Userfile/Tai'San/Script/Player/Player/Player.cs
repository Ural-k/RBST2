using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ì¸óÕ
/// </summary>
public class Player : PlayerBase
{
    private void Update()
    {
        PlayerMove();
        if (Input.GetMouseButtonDown(2)) { skill2_ = OnSkill(skill2_); }//âºÅ´
        if (Input.GetMouseButtonDown(1)) { skill3_ = OnSkill(skill3_); }
    }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed) skill1_ = OnSkill(skill1_); }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (context.performed) skill2_ = OnSkill(skill2_); }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (context.performed) skill3_ = OnSkill(skill3_); }
}