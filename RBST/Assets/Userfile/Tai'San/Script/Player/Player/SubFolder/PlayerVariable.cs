using UnityEngine;

/// <summary>
/// プレイヤーの変数関連
/// </summary>
public class PlayerVariable : MonoBehaviour
{
    protected const int INPUT_SKILL_ONE         = 1;
    protected const int INPUT_SKILL_TWO         = 2;
    protected const int INPUT_SKILL_THREE       = 3;
    protected const int RANDOME_DAMAGE_RANGE    = 20;
    protected const float MOVE_SCREEN_X         = 8.8f;
    protected const float MOVE_SCREEN_Y         = 4.8f;
    protected const float DOWN_TIME             = 5.0f;
    protected const float ACTIVE_COMBO_SECOND   = 3.0f;

    [SerializeField] protected bool demodebug_;//仮
    [SerializeField] protected PlayerInfo info_;

    public PlayerInfo GetInfo { get { return info_; } }
}