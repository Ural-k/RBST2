using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー全般
/// </summary>
/// <remarks>派生クラス</remarks>
public class Player : PlayerBase, IDamageable
{
    [SerializeField] PlayerAttack playerAttack_;

    TargetToEnemy targetToEnemy_;
    InputAction inputMove_;

    void Start()
    {
        PlayerManager.AddPlayer(this);
        inputMove_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out targetToEnemy_);
    }
    void Update()
    {
        PlayerMove(inputMove_.ReadValue<Vector2>());
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerAttack_.CircleAttack(targetToEnemy_.GetTargetTransform,Vector2.one * 3);
    }
    
    public void TakeDamage(int damage_)
    {
        Debug.Log("当たっちゃったーワイプワイプ");
    }

}