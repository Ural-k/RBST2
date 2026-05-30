using UnityEngine.InputSystem;

/// <summary>
/// “ü—Í
/// </summary>
public class Player : PlayerBase
{
    private void Update() { PlayerMove(); }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed) skill1_ = OnSkill(skill1_); }
    public void InputAttack2(InputAction.CallbackContext context) 
    { if (context.performed) skill2_ = OnSkill(skill2_); }
    public void InputAttack3(InputAction.CallbackContext context) 
    { if (context.performed) skill3_ = OnSkill(skill3_); }
}