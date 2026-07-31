using Unity.Netcode;
using UnityEngine;

/// <summary>
/// プレイヤーの変数関連
/// </summary>
public class PlayerVariable : NetworkBehaviour
{
    protected const int INPUT_SKILL_ONE         = 1;
    protected const int INPUT_SKILL_TWO         = 2;
    protected const int INPUT_SKILL_THREE       = 3;
    protected const int RANDOME_DAMAGE_RANGE    = 20;
    protected const float MOVE_SCREEN_X         = 8.8f;
    protected const float MOVE_SCREEN_Y         = 4.8f;
    protected const float DOWN_TIME             = 5.0f;
    protected const float ACTIVE_COMBO_SECOND   = 3.0f;

    [SerializeField] public int debugJobChangeNumber_;//デバッグ用
    [SerializeField] protected BetaPlayerIcon playerIcon_;
    [SerializeField] protected PlayerInfo info_;

    [Header("アニメーション")]
    [SerializeField]
    protected Animator animator_;

    protected static readonly int IS_MOVING_HASH
    = Animator.StringToHash("IsMoving");

    protected static readonly int ATTACK_HASH
        = Animator.StringToHash("Attack");

    protected static readonly int IDLE_HASH
        = Animator.StringToHash("Idle");

    public PlayerInfo GetInfo { get { return info_; } }
}