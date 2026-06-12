using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの変数関連
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] protected bool demodebug_;//仮

    [System.Serializable]//入力ごとのスキル情報
    public struct SkillInstance
    {
        public float cd_;
        public int nowCombo_;
    }
    [SerializeField] protected Parameter parameter_;
    protected float gcd_;
    protected SkillInstance skill1_;
    protected SkillInstance skill2_;
    protected SkillInstance skill3_;
    protected JobData skillData_;
    protected InputAction inputAxis_;
    protected Vector2 lastFace_;

    /*
     :  プロパティ
     */
    public JobData GetJobData { get { return skillData_; } }
    public float GetGCD { get { return gcd_; } }
    public SkillInstance GetSkillInstance1 { get { return skill1_; } }
    public SkillInstance GetSkillInstance2 { get { return skill2_; } }
    public SkillInstance GetSkillInstance3 { get { return skill3_; } }
    public Vector2 GetLastFace { get { return lastFace_; } }
}