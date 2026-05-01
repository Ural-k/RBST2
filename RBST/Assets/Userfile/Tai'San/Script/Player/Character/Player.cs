using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー全般
/// </summary>
/// <remarks>派生クラス</remarks>
public class Player : PlayerBase
{
    [SerializeField] PlayerAttack playerAttack_;

    CharacterController characterController_;
    TargetToEnemy targetToEnemy_;
    InputAction inputMove_;

    void Start()
    {
        inputMove_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out characterController_);
        TryGetComponent(out targetToEnemy_);
    }
    void Update()
    {
        PlayerMove(inputMove_.ReadValue<Vector2>(), characterController_);
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerAttack_.CircleAttack(targetToEnemy_.GetTargetTransform,Vector2.one * 3);
    }
}