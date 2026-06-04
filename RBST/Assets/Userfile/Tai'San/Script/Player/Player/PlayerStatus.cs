using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの変数関連
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] protected bool debug_;//仮

    [System.Serializable]//プレイヤー自身の状態数値
    protected struct Parameter
    {
        public int jobNumber_;          //職業
        public string playerName_;      //プレイヤーの名前x
        public float maxHp_;            //最大HPx
        public float hp_;               //現在のHPx
        public float speed_;            //移動速度o
        public float attack_;           //攻撃力x
        public float defense_;          //防御力x
        public float critical_;         //クリティカル率x
        public uint lv_;                //現在のレベルx
    }
    [System.Serializable]//入力ごとのスキル情報
    public struct SkillInstance
    {
        public float cd_;
        public int input_;
        public int nowCombo_;
    }
    [SerializeField] protected Parameter parameter_;
    [SerializeField] protected float gcd_;
    [SerializeField] protected SkillInstance skill1_;
    [SerializeField] protected SkillInstance skill2_;
    [SerializeField] protected SkillInstance skill3_;
    protected JobData skillData_;
    protected InputAction inputAxis_;

    /*
     :  プロパティ
     */
    public float GetGCD { get { return gcd_; } }
    public SkillInstance GetSkillInstance1 { get { return skill1_; } }
    public SkillInstance GetSkillInstance2 { get { return skill2_; } }
    public SkillInstance GetSkillInstance3 { get { return skill3_; } }
}