using UnityEngine.InputSystem;

/// <summary>
/// “ü—Í
/// </summary>
public class Player : PlayerBase
{
    private void Update()
    {
        PlayerMove();
#if UNITY_EDITOR
        info_.JobChange(debugJobChangeNumber_);//“r’†‚ÅƒWƒ‡ƒu‚ğ•Ï‚¦‚½‚Æ‚«‚ÌØ‚è‘Ö‚¦
#endif
    }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed && info_.downTime_ == 0) OnInputSkill(out info_.skill1_, INPUT_SKILL_ONE); }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (context.performed && info_.downTime_ == 0) OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO); }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (context.performed && info_.downTime_ == 0) OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE); }
}