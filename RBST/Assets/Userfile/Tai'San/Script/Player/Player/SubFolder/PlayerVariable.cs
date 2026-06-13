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

    /*
     :  プロパティ
     */
    public JobData          GetJobData          { get { return info_.skillData_; } }
    public float            GetGCD              { get { return info_.gcd_; } }
    public InputSkillInfo   GetSkillInstance1   { get { return info_.skill1_; } }
    public InputSkillInfo   GetSkillInstance2   { get { return info_.skill2_; } }
    public InputSkillInfo   GetSkillInstance3   { get { return info_.skill3_; } }
    public Vector2          GetLastFace         { get { return info_.lastFace_; } }
}