using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルの取得
/// </summary>
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Player/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private List<SkillInfo1> skillInfo_ = new List<SkillInfo1>();
    public static SkillInfo1 GetSkillInfo(SkillName attackName)
    { return Resources.Load<SkillData>("SkillData").skillInfo_[(int)attackName]; }
}

/*
 :  スキル名　※スキルを増やす場合このenumだけ追加する必要がある
 */
public enum SkillName
{
    Fire,
    Fire2,
    Null,
}

/*
 :  攻撃対象
 */
public enum TargetType
{
    Enemy,
    Player,
    Natural,
    Null
}

[System.Serializable]
public struct MotionInfo
{
    public float time_;
    public float speed_;
    public bool jumpOn_;
    public AnimationCurve jumpOrbit_;
}

/*
 :  スキル設定
 */
[System.Serializable]
public struct SkillInfo1
{                                       //役割[参照する強化値(変動する値)]
    /* 追加する値メモ
     :  ・offset,rotate
     :  ・inputで向き指定
     :  ・マウス
     */

    //固定ステータス
    [Header("[ToolTip、あります。]")]
    [Tooltip("表示名")]                        public string name_;                //△
    [Tooltip("演出プレファブ")]                 public GameObject particle_;        //o
    [Tooltip("攻撃対象")]                      public TargetType targetType_;       //o
    [Tooltip("次回の攻撃")]                    public SkillName combo_;            //o
    //public Buff buff_;                  //付与するバフx
    //public DeBuff deBuff_;              //付与するデバフx
    [Tooltip("近い攻撃対象を中心に")]            public bool toTarget_;              //o

    //範囲
    [Tooltip("矩形比率")]                       public Vector2 aspect_;             //o
    [Tooltip("半径")]                          public float radius_;               //o

    //攻撃パラメータ
    [Tooltip("威力値")]                        public int power_;                  //△
    [Tooltip("GCD")]                          public float gcd_;                  //o
    [Tooltip("CD")]                           public float cd_;                   //o
    [Tooltip("発動タイミング")]                 public float diray_;                //x
    [Tooltip("発動後の動き")]                   public MotionInfo motion_;          //o

    /// <summary>
    /// Info2の情報を元にInfo1書き換え※使うか未定
    /// </summary>
    /// <param name="versions">変動内容</param>
    /// <param name="dynamic">動的変動(固定値は使えない)</param>
    public void InfoSet(List<SkillInfo2> versions, bool dynamic = true)
    {
        /*
         :[単純強化]
         :  <合算フェーズ>
         :   固定ステータスを取得
         :          ↓
         :   強化の変化量・倍率を合算
         :          ↓
         :  <割り当てフェーズ>
         :   固定値割り当て
         :          ↓
         :   変化量割り当て
         :          ↓
         :   変化倍率割り当て
         */
        SkillInfo2 allVarsion = new SkillInfo2(); //最終変化量
        int setflag = 0;
        foreach (SkillInfo2 oneVersion in versions)//固定ステータスをセット(古い方優先)
        {
            if (!dynamic)
            {
                if ((setflag & (1 << 0)) == 0 && oneVersion.setPower_ != 0)
                { setflag |= 1 << 0; allVarsion.setPower_ = oneVersion.setPower_; }
                if ((setflag & (1 << 1)) == 0 && oneVersion.setGcd_ != 0)
                { setflag |= 1 << 1; allVarsion.setGcd_ = oneVersion.setGcd_; }
                if ((setflag & (1 << 2)) == 0 && oneVersion.setCd_ != 0)
                { setflag |= 1 << 2; allVarsion.setCd_ = oneVersion.setCd_; }
                if ((setflag & (1 << 3)) == 0 && oneVersion.setSpeed_ != 0)
                { setflag |= 1 << 3; allVarsion.setSpeed_ = oneVersion.setSpeed_; }
            }
            //強化の変化量・倍率を合算
            allVarsion.versionScale_        += oneVersion.versionScale_;
            allVarsion.versionPower_        += oneVersion.versionPower_;
            allVarsion.versionGcd_          += oneVersion.versionGcd_;
            allVarsion.versionCd_           += oneVersion.versionCd_;
            allVarsion.versionSpeed_        += oneVersion.versionSpeed_;
            allVarsion.ratePower_   *= oneVersion.ratePower_;
            allVarsion.rateGcd_     *= oneVersion.rateGcd_;
            allVarsion.rateCd_      *= oneVersion.rateCd_;
            allVarsion.rateSpeed_   *= oneVersion.rateSpeed_;
        }

        //--<割り当て>-------------------------------------------------------------------
        //固定値割り当て
        if (!dynamic)
        {
            power_  = allVarsion.setPower_  == 0 ? power_   : allVarsion.setPower_;
            gcd_    = allVarsion.setGcd_    == 0 ? gcd_     : allVarsion.setGcd_;
            cd_     = allVarsion.setCd_     == 0 ? cd_      : allVarsion.setCd_;
        }
        //変化量割り当て
        if (aspect_ != Vector2.zero) aspect_ += Vector2.one * allVarsion.versionScale_;
        if (radius_ != 0) radius_ += allVarsion.versionScale_;
        power_ += allVarsion.versionPower_;
        gcd_    += allVarsion.versionGcd_;
        cd_     += allVarsion.versionCd_;
        //変化倍率割り当て
        power_  = (int)(power_ * allVarsion.ratePower_);
        gcd_    *= allVarsion.rateGcd_;
        cd_     *= allVarsion.rateCd_;
    }
}

//操作する強化値(スクタブ用※使うか未定)
[System.Serializable]
public struct SkillInfo2
{
    public SkillInfo1 info1_;          //初期値と変化量

    //変化量
    public int setPower_;               //固定(攻撃力)
    public float setGcd_;               //固定(gcd)
    public float setCd_;                //固定(cd)
    public float setSpeed_;             //固定(スピード)

    public int versionScale_;           //変化量(スケール)
    public int versionPower_;           //変化量(攻撃力)
    public float versionGcd_;           //変化量(gcd)
    public float versionCd_;            //変化量(cd)
    public float versionSpeed_;         //変化量(スピード)

    public float ratePower_;            //変化倍率(攻撃力)
    public float rateGcd_;              //変化倍率(gcd)
    public float rateCd_;               //変化倍率(cd)
    public float rateSpeed_;            //変化倍率(スピード)
}