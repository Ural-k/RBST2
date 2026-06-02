using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ì¸óÕ
/// </summary>
public class Player : PlayerBase
{
    private void Update()
    {
        if(debug_) { skillData_ = JobData.GetJobSkill(parameter_.jobNumber_); }
        PlayerMove();
        if (Input.GetMouseButtonDown(1)) { skill2_ = OnSkill(skill2_, skillData_.GetSkill2()); }//âºÅ´
        if (Input.GetMouseButtonDown(2)) { skill3_ = OnSkill(skill3_, skillData_.GetSkill3()); }
    }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed) skill1_ = OnSkill(skill1_, skillData_.GetSkill1()); }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (context.performed) skill2_ = OnSkill(skill2_, skillData_.GetSkill2()); }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (context.performed) skill3_ = OnSkill(skill3_, skillData_.GetSkill3()); }
}