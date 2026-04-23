using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerBase
{
    CharacterController characterController_;
    InputAction inputMove_;

    void Start()
    {
        inputMove_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out characterController_);
    }
    void Update()
    {
        PlayerMove(inputMove_.ReadValue<Vector2>(), characterController_);
    }



    //public void InputSpecial(InputAction.CallbackContext context)
    //{
    //    if(!context.performed) return;
    //    //Special();
    //}
}