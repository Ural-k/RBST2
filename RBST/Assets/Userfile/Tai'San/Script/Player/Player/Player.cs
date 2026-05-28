using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    private void Update() { PlayerMove(); }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed) actionState_.action1_ = OnAction(actionState_.action1_); }
    public void InputAttack2(InputAction.CallbackContext context) 
    { if (context.performed) actionState_.action2_ = OnAction(actionState_.action2_); }
    public void InputAttack3(InputAction.CallbackContext context) 
    { if (context.performed) actionState_.action3_ = OnAction(actionState_.action3_); }
}