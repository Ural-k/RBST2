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
    JumpOnFire,
    CenterFire,
    DynamicFire,
    BeamFire,
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
    //[Tooltip("発動タイミング")]                 public float diray_;                //x
    [Tooltip("発動後の動き")]                   public MotionInfo motion_;          //o
}
