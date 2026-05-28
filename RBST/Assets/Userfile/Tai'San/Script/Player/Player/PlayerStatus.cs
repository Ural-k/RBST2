using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    protected struct Parameter
    {
        public string jobName_;         //職業名
        public string playerName_;      //プレイヤーの名前
        public float maxHp_;            //最大HP
        public float hp_;               //現在のHP
        public float speed_;            //移動速度
        public float attack_;           //攻撃力
        public float defense_;          //防御力
        public float critical_;         //クリティカル率
        public uint lv_;                //現在のレベル
    }
    [System.Serializable]
    protected struct ActionState
    {
        public float gcd_, cd1_, cd2_, cd3_;
        public Action action1_, action2_, action3_;
    }
    [SerializeField] protected Parameter parameter_;
    [SerializeField] protected ActionState actionState_;
    protected InputAction inputAxis_;

}