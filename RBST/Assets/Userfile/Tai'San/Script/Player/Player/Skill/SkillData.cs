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
    [Header("基本")]
    public string       name_;
    public int          power_;
    public float        gcd_;
    public float        cd_;
    public bool         comboKeep_;

    [Header("ターゲット")]
    public EntityType   targetType_;
    public SkillShape   shape_;
    public Vector2      offset_;
    public Vector2      scale_;
    public bool         toTarget_;

    [Header("追加/特殊スキル")]
    public GameObject   subSkill_;

    public MotionInfo   motion_;
}