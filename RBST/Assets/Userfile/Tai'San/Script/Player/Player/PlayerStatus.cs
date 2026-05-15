using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    protected struct Parameter
    {
        public string jobName_;         //職業名
        public string playerName_;      //プレイヤーの名前
        public float hp_;
        public float speed_;            //移動速度
        public float attack_;           //攻撃力
        public float defense_;          //防御力
        public float critical_;         //クリティカル率
        public uint lv_;               //現在のレベル
    }

    protected void SubtractionParameter(Parameter p)
    {
        parameter_.hp_ += p.hp_;
        parameter_.speed_ += p.speed_;
        parameter_.attack_ += p.attack_;
        parameter_.defense_ += p.defense_;
        parameter_.critical_ += p.critical_;
        parameter_.lv_ += p.lv_;
        Debug.Log("デバフ発動");
    }

    [Header("パラメータ")]
    [SerializeField] protected Parameter parameter_;
    protected enum TargetType
    {
        Enemy,Player,Natural
    }

    protected InputAction   inputAxis_;         //移動キー入力
    protected Transform     target_ = null;     //ターゲット中のTransfrom

    [Header("ターゲット関連")]
    [SerializeField] protected GameObject   targetersObject_;   //ターゲット可能なオブジェクト群の親
    [SerializeField] protected Transform    targetGraphic_;     //ターゲット表示
}