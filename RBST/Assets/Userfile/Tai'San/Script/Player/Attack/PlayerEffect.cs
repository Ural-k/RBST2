using UnityEngine;

//作り直したいEffectシステム
public class PlayerEffect : MonoBehaviour
{


}

public enum Buff
{
    Null,
}

public enum DeBuff
{
    Null,
    Thunder,
}

public enum EffectType
{
    Buff,
    Debuff,
}

//参照値
public struct EffectInfo1
{
    //固定ステータス
    public string name_;

    //情報
    public bool duplicate_;        //重複可能
    public float lifeTimne_;       //付与時間
    public float interval_;        //発動間隔
    public EffectType type_;       //種類

    //効果
    public float power_;        //攻撃力
    public float deffence_;     //防御力
    public float speed_;        //移動速度
    public float hp_;           //HP
    public float maxHp_;        //最大HP
    public float crtical_;      //クリティカル率
    public float gcd_;          //GCD
    public float cd_;           //CD
}

//変化値(仮置き)
public struct EffectInfo2
{
    EffectInfo1 info1_;


}

