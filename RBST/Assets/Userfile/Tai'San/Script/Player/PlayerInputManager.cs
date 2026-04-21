using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : PlayerSystem
{
    [SerializeField] Job job_;
    [SerializeField] JobStatus jobStatus_;
    PlayerStatusBase status_;

    CharacterController characterController_;
    InputAction inputMove_;

    void Start()
    {
        status_ = jobStatus_.GetStatus(job_);
        inputMove_ = InputSystem.actions.FindAction("Move");
        TryGetComponent(out characterController_);
    }

    void Update()
    {
        PlayerMove(inputMove_.ReadValue<Vector2>(),status_.MoveSpeed,characterController_);
    }

    //ï∂éöóÒÇ©ÇÁEnumÇ…ïœä∑ÇµÇΩÇ¢
    //void OnValidate()
    //{
    //    job_ = (Job)System.Enum.Parse(typeof(Job), "JJJ", true);
    //}

    /// <summary>
    /// ÉvÉåÉCÉÑÅ[à⁄ìÆ
    /// </summary>
    //public void PlayerMove()
    //{
    //    Vector2 move_value = input_move_.ReadValue<Vector2>();
    //    move_value *= status_.move_speed_ * Time.deltaTime;
    //    character_controller_.Move(move_value);
    //}

    public void Primary(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

    }

    public void Secondary(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

    }

    public void Special(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

    }
}