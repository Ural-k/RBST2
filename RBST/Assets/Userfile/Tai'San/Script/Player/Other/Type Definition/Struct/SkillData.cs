using UnityEngine;

/*
 :  スキル設定
 */
[System.Serializable]
public struct SkillData
{                                       //役割[参照する強化値(変動する値)]
    /* 追加する値メモ
     :  ・offset,rotate
     :  ・inputで向き指定
     :  ・マウス
     */

    //固定ステータス
    [Tooltip("表示名")] public string name_;                                    //o
    [Tooltip("演出プレファブ")] public ParticleSystem particle_;                     //o
    [Tooltip("攻撃対象")] public TargetType targetType_;                        //o
    [Tooltip("形状")] public SkillShape shape_;                                //o
    [Tooltip("近い攻撃対象を中心に")] public bool toTarget_;                      //o

    //範囲
    [Tooltip("プレイヤー向き基準")] public bool baseDirection_;                   //△
    [Tooltip("中心")] public Vector2 offset_;                                   //x
    [Tooltip("矩形比率")] public Vector2 scale_;                                 //o
    [Tooltip("半径")] public float radius_;                                     //o

    //攻撃パラメータ
    [Tooltip("威力値")] public int power_;                                       //o
    [Tooltip("GCD")] public float gcd_;                                         //o
    [Tooltip("CD")] public float cd_;                                           //o
    //[Tooltip("発動タイミング")]                 public float diray_;            //x

    public MotionInfo motion_;                          //o
}
