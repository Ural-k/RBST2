using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Job job_;
    [SerializeField] JobStatus job_status_;
    PlayerStatusBase status_;

    CharacterController character_controller_;
    InputAction input_move_;

    void Start()
    {
        status_ = job_status_.GetStatus(job_);
        input_move_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out character_controller_);
    }

    void Update()
    {
        PlayerMove();
    }

    private void OnValidate()
    {
        //if (MaterialList != null) CreateMaterialNameList();
    }

    /// <summary>
    /// ÉvÉåÉCÉÑÅ[à⁄ìÆ
    /// </summary>
    public void PlayerMove()
    {
        Vector2 move_value = input_move_.ReadValue<Vector2>();
        move_value *= status_.move_speed_ * Time.deltaTime;
        character_controller_.Move(move_value);
    }

    public void Primary(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

    }

}