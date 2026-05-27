using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    private void Update() { PlayerMove(); }

    public void InputAttack1(InputAction.CallbackContext context) { if (context.performed) OnAction(Action.Fire); }
    //public void InputAttack2(InputAction.CallbackContext context) { if (context.performed)  }
    //public void InputAttack3(InputAction.CallbackContext context) { if (context.performed)  }
}