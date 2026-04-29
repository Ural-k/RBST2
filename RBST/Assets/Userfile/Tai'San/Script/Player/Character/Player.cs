using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー全般
/// </summary>
/// <remarks>派生クラス</remarks>
public class Player : PlayerBase
{
    CharacterController characterController_;
    InputAction inputMove_;
    [SerializeField] PlayerAttack playerAttack_;

    void Start()
    {
        inputMove_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out characterController_);
    }
    void Update()
    {
        PlayerMove(inputMove_.ReadValue<Vector2>(), characterController_);
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerAttack_.CircleAttack(transform, Vector2.right * 4.5f, Vector2.one * 3);
    }
}