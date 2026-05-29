using UnityEngine;
using UnityEngine.InputSystem;

//変数宣言
public class PlayerStatus : MonoBehaviour
{
    //メモ:ジョブごとの初期スキルを持たすためにSkillNameをジョブごとに分ける必要がある

    [System.Serializable]//プレイヤー自身の状態数値
    protected struct Parameter
    {
        public string jobName_;         //職業名x
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
        public SkillName skillName_;
    }
    //[System.Serializable]
    //public struct SkillMotion//スキルモーション中の状態変化
    //{
    //    [Tooltip("モーション時間")]
    //    public float time_;
    //    [Tooltip("移動速度")]
    //    public float speed_;
    //    [HideInInspector]
    //    public Vector2 toPosition;
    //}
    [SerializeField] protected float gcd_;
    [SerializeField] protected Parameter parameter_;
    [SerializeField] protected SkillInstance skill1_;
    [SerializeField] protected SkillInstance skill2_;
    [SerializeField] protected SkillInstance skill3_;
    //[SerializeField] protected SkillMotion motion_;
    protected InputAction inputAxis_;
    protected float motionTimer_ = 0;
    protected Vector3 deltaPosition_;

    /*
     :  プロパティ
     */
    public float GetGCD { get { return gcd_; } }
    public SkillInstance GetSkillInstance1 { get { return skill1_; } }
    public SkillInstance GetSkillInstance2 { get { return skill2_; } }
    public SkillInstance GetSkillInstance3 { get { return skill3_; } }
}