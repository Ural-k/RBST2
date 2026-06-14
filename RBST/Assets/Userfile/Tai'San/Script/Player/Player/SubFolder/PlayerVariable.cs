using UnityEngine;

/// <summary>
/// プレイヤーの変数関連
/// </summary>
public class PlayerVariable : MonoBehaviour
{
    protected const int INPUT_SKILL_ONE     = 1;
    protected const int INPUT_SKILL_TWO     = 2;
    protected const int INPUT_SKILL_THREE   = 3;

    [SerializeField] protected bool demodebug_;//仮
    [SerializeField] protected PlayerInfo info_;

    public PlayerInfo       GetInfo             { get { return info_; } }
}