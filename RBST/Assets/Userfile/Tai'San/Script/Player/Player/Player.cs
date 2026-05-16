using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    private void Update()
    {
        PlayerMove();
        if (Input.GetKeyDown(KeyCode.T)) OnTarget();
        if (Input.GetKeyUp(KeyCode.Space)) OnTarget();  //デバック用仮処理↓↓
        //if(Input.GetKeyDown(KeyCode.B)) TakeMeBuff();
    }

    //public void InputAttack1(InputAction.CallbackContext context) { if (context.performed)  }
    //public void InputAttack2(InputAction.CallbackContext context) { if (context.performed)  }
    //public void InputAttack3(InputAction.CallbackContext context) { if (context.performed)  }
}