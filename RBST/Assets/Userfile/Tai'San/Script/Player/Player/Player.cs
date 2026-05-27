using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    public Transform GetTransform() { return transform; }

    private void Update()
    {
        PlayerMove();
    }

    public void InputAttack1(InputAction.CallbackContext context) { if (context.performed) OnAction(Action.Fire); }
    //public void InputAttack2(InputAction.CallbackContext context) { if (context.performed)  }
    //public void InputAttack3(InputAction.CallbackContext context) { if (context.performed)  }
}