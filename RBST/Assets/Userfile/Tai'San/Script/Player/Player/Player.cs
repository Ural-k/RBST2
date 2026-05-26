using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    public Transform GetTransform() { return transform; }

    private void Update()
    {
        PlayerMove();
        if(Input.GetKeyDown(KeyCode.Space)) OnAction(Action.Fire);
        //if (Input.GetKeyUp(KeyCode.Space)) OnTarget();  //デバック用仮処理↓↓
    }

    //public void InputAttack1(InputAction.CallbackContext context) { if (context.performed) OnAction(Action.Fire); }
    //public void InputAttack2(InputAction.CallbackContext context) { if (context.performed)  }
    //public void InputAttack3(InputAction.CallbackContext context) { if (context.performed)  }
}