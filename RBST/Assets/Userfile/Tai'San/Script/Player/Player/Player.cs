using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ì¸óÕ
/// </summary>
public class Player : PlayerBase
{
    private void Update()
    {

        PlayerMove();
#if UNITY_EDITOR
        info_.JobChange(debugJobChangeNumber_);//ìríÜÇ≈ÉWÉáÉuÇïœÇ¶ÇΩÇ∆Ç´ÇÃêÿÇËë÷Ç¶
#endif
    }
    
    public void InputAttack1(InputAction.CallbackContext context)
    {
      if (!IsOwner) return; 
      if ( context.performed && info_.downTime_ == 0) AttackServerRpc(INPUT_SKILL_ONE); 
    }
    public void InputAttack2(InputAction.CallbackContext context)
    { if (!IsOwner) return; 
      if ( context.performed && info_.downTime_ == 0) AttackServerRpc(INPUT_SKILL_TWO); 
    }
    public void InputAttack3(InputAction.CallbackContext context)
    { if (!IsOwner) return; 
      if ( context.performed && info_.downTime_ == 0) AttackServerRpc(INPUT_SKILL_THREE); 
    }

    [ServerRpc]
    private void AttackServerRpc(int skillNumber)
    {
        AttackClientRpc(skillNumber);
    }

    [ClientRpc]
    private void AttackClientRpc(int skillNumber)
    {
        if (skillNumber == INPUT_SKILL_ONE)
        {
            OnInputSkill(out info_.skill1_, INPUT_SKILL_ONE);
        }
        else if (skillNumber == INPUT_SKILL_TWO)
        {
            OnInputSkill(out info_.skill2_, INPUT_SKILL_TWO);
        }
        else if (skillNumber == INPUT_SKILL_THREE)
        {
            OnInputSkill(out info_.skill3_, INPUT_SKILL_THREE);
        }
    }
}