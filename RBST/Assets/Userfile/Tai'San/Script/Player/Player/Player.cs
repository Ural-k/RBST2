using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ì¸óÕ
/// </summary>
public class Player : PlayerBase
{
    private JobData demomemory_ = null;

    //debugóp
    private void Awake()
    {
        info_.parameter_.HP = 1000;
    }
    private void Update()
    {
#if UNITY_EDITOR
        info_.JobChange(debugJobChangeNumber_);//ìríÜÇ≈ÉWÉáÉuÇïœÇ¶ÇΩÇ∆Ç´ÇÃêÿÇËë÷Ç¶
#endif
        if (/*GameSceneManager.Instance.State == GameState.isPlaying*/info_.downTime_ == 0)
        {
            PlayerMove();
            if (Input.GetMouseButtonDown(1)) { OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO); }//âºÅ´
            if (Input.GetMouseButtonDown(2)) { OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE); }
        }

        if (Input.GetKeyDown(KeyCode.R)) { EnemyManager.DeleteAllEnemy(); }
    }

    public void InputAttack1(InputAction.CallbackContext context)
    { if (context.performed && info_.downTime_ == 0) OnInputSkill(out info_.skill1_, INPUT_SKILL_ONE); }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (context.performed) OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO); }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (context.performed) OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE); }
}