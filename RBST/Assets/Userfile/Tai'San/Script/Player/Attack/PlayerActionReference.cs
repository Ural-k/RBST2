using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionReference", menuName = "ScriptableObjects/Player/PlayerActionReference")]
public class PlayerActionReference : ScriptableObject
{
    [SerializeField] private List<ActionInfo1> actionInfo_ = new List<ActionInfo1>();
    public ActionInfo1 GetActionInfo(Action attackName) { return actionInfo_[(int)attackName]; }
}

public enum Action
{
    Fire,
    Thunder,
    Bit,
    BitUpgrade,
    Null,
}

//攻撃対象
public enum TargetType
{
    Enemy,
    Player,
    Natural,
    Null
}
 

//１つ攻撃に必要な基底の値と強化情報
[System.Serializable]
public struct ActionInfo1
{                                       //役割[参照する強化値(変動する値)]
    /* 追加する値メモ
     :  ・突進するかどうか
     :  ・ターゲットの座標に設置
     :  ・offset,rotate
     :  ・inputで向き指定
     :  ・マウス
     */

    //固定ステータス
    public string name_;                //攻撃名△
    public TargetType targetType_;      //実行対象o
    public Action combo_;               //次に実行できるようになる技
    //public Buff buff_;                  //付与するバフx
    //public DeBuff deBuff_;              //付与するデバフx
    public bool toTarget_;              //ターゲット中心o
    public bool jumpOn_;                //飛びつき

    //範囲
    public Vector2 aspect_;             //初期アスペクト比[versionAspect,scale]o
    public float radius_;               //初期半径[varsionRadius,scale]o

    //攻撃パラメータ
    public int power_;                  //攻撃力[power]△
    public float gcd_;                  //GCD(0にするとGCDの干渉を受けなくなる)[gcd]x
    public float cd_;                   //CD[cd]x
    public float speed_;                //攻撃時のスピード[speed]x

    /// <summary>
    /// Info2の情報を元にInfo1書き換え
    /// </summary>
    /// <param name="versions">変動内容</param>
    /// <param name="dynamic">動的変動(固定値は使えない)</param>
    public void InfoSet(List<ActionInfo2> versions, bool dynamic = true)
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
        ActionInfo2 allVarsion = new ActionInfo2(); //最終変化量
        int setflag = 0;
        foreach (ActionInfo2 oneVersion in versions)//固定ステータスをセット(古い方優先)
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
            speed_  = allVarsion.setSpeed_  == 0 ? speed_   : allVarsion.setSpeed_;
        }
        //変化量割り当て
        if (aspect_ != Vector2.zero) aspect_ += Vector2.one * allVarsion.versionScale_;
        if (radius_ != 0) radius_ += allVarsion.versionScale_;
        power_ += allVarsion.versionPower_;
        gcd_    += allVarsion.versionGcd_;
        cd_     += allVarsion.versionCd_;
        speed_  += allVarsion.versionSpeed_;
        //変化倍率割り当て
        power_  = (int)(power_ * allVarsion.ratePower_);
        gcd_    *= allVarsion.rateGcd_;
        cd_     *= allVarsion.rateCd_;
        speed_  *= allVarsion.rateSpeed_;
    }
}

//操作する強化値(スクタブ用)
[System.Serializable]
public struct ActionInfo2
{
    public ActionInfo1 info1_;          //初期値と変化量

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