using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    CharacterController character_controller_;
    InputAction input_move_;

    void Start()
    {
        input_move_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out character_controller_);
    }

    void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        Vector2 move_value = input_move_.ReadValue<Vector2>();
        move_value *= 5 * Time.deltaTime;
        character_controller_.Move(move_value);
    }

    public void InputDemo(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

    }
}
