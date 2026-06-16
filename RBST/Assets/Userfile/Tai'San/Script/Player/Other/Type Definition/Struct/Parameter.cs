using UnityEngine;

[System.Serializable]//プレイヤーの状態数値
public struct Parameter
{
    public int jobNumber_;          //職業
    public string playerName_;      //プレイヤーの名前x
    public int maxHp_;              //最大HPx
    private int hp_;                 //現在のHPx
    public float speed_;            //移動速度o
    public float attack_;           //攻撃力x
    public float defense_;          //防御力x
    public float critical_;         //クリティカル率x
    public uint lv_;                //現在のレベルx

    /// <summary>
    /// 0～maxHp
    /// </summary>
    public int HP { get { return hp_; } set { hp_ = Mathf.Clamp(value, 0, maxHp_); } }
}
