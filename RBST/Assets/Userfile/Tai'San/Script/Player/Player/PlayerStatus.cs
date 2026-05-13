using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] protected string   jobName_;      //職業名
    [SerializeField] protected string   playerName_;   //プレイヤーの名前
    [SerializeField] protected float    speed_;        //移動速度
    [SerializeField] protected float    attack_;       //攻撃力
    [SerializeField] protected float    defense_;      //防御力
    [SerializeField] protected float    critical_;     //クリティカル率
    [SerializeField] protected float    lv_;           //現在のレベル

    protected InputAction   inputAxis_;         //移動キー入力
    protected Transform     target_ = null;     //ターゲット中のTransfrom

    [Header("ターゲット関連")]
    [SerializeField] protected GameObject   targetersObject_;   //ターゲット可能なオブジェクト群の親
    [SerializeField] protected Transform    targetGraphic_;     //ターゲット表示
}