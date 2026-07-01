using UnityEngine;

public class SubSkill : MonoBehaviour
{

    
}

public struct SubSkillInfo
{
    [SerializeField] private float delay_;
    [Header("基本")]
    [Tooltip("表示名")] public string name_;                                    //o
    [Tooltip("追加のスキル")] public GameObject subSkill_;                       //x
    [Tooltip("コンボが途切れない")] public bool comboKeep_;                       //o
    [Tooltip("威力値")] public int power_;                                       //o

    [Header("ターゲット")]
    [Tooltip("近い攻撃対象を中心に")] public bool toTarget_;                      //o
    [Tooltip("攻撃対象")] public TargetType targetType_;                        //o
    [Tooltip("形状")] public SkillShape shape_;                                 //o
    [Tooltip("中心")] public Vector2 offset_;                                   //x
    [Tooltip("矩形比率")] public Vector2 scale_;                                 //o

    private PlayerInfo playerInfo_;

}