using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour
{
    protected struct Status
    {
        public string jobName_;         //職業名
        public string playerName_;      //プレイヤーの名前
        public float speed_;            //移動速度
        public float attack_;           //攻撃力
        public float defense_;          //防御力
        public float critical_;         //クリティカル率
        public float lv_;               //現在のレベル
    }
    [Header("ステータス")]
    [SerializeField] protected Status status_;

    protected InputAction   inputAxis_;         //移動キー入力
    protected Transform     target_ = null;     //ターゲット中のTransfrom

    [Header("ターゲット関連")]
    [SerializeField] protected GameObject   targetersObject_;   //ターゲット可能なオブジェクト群の親
    [SerializeField] protected Transform    targetGraphic_;     //ターゲット表示
}