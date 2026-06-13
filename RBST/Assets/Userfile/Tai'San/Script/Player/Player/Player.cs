using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ì¸óÕ
/// </summary>
public class Player : PlayerBase
{
    private JobData demomemory_ = null;
    private void Update()
    {
#if UNITY_EDITOR
        info_.skillData_ = JobData.GetJobSkill(info_.parameter_.jobNumber_); //ìríÜÇ≈ÉWÉáÉuÇïœÇ¶ÇΩÇ∆Ç´ÇÃêÿÇËë÷Ç¶
        if(info_.skillData_ != demomemory_)
        {
            info_.skill1_.nowCombo_ = 0;
            info_.skill2_.nowCombo_ = 0;
            info_.skill3_.nowCombo_ = 0;
            demomemory_ = info_.skillData_;
        }
#endif
        if (/*GameSceneManager.Instance.State == GameState.isPlaying*/true)
        {
            PlayerMove();
            if (Input.GetMouseButtonDown(1)) { OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO); }//âºÅ´
            if (Input.GetMouseButtonDown(2)) { OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE); }
        }
    }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed) OnInputSkill(out info_.skill1_, INPUT_SKILL_ONE); }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (context.performed) OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO); }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (context.performed) OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE); }
}