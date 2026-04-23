using UnityEngine;

/*
 :  基底クラス
 */
public class PlayerBase : MonoBehaviour
{                                               //ゲーム内表記
    [SerializeField] float moveSpeed_;          //MOV
    [SerializeField] float hp_;                 //HP
    [SerializeField] float attack_;             //ATK
    [SerializeField] float defense_;            //DEF
    [SerializeField] float criticalPercent_;    //CRT

    /// <summary>
    /// 移動速度
    /// </summary>
    /// <remarks>property:0~x</remarks>
    protected float MoveSpeed { get { return moveSpeed_; } set { moveSpeed_ = value <= 0 ? 0 : value; } }
    /// <summary>
    /// 攻撃力
    /// </summary>
    /// <remarks>property:0~x</remarks>
    protected float Attack { get { return attack_; } set { attack_ = value <= 0 ? 0 : value; } }
    /// <summary>
    /// 防御力
    /// </summary>
    ///<remarks>property:nothing</remarks>
    protected float Defence { get { return defense_; } set { defense_ = value; } }
    /// <summary>
    /// クリティカル率
    /// </summary>
    /// <remarks>property:0.00~1.00</remarks>
    protected float CriticalPercent
    { 
        get { return criticalPercent_; } 
        set 
        { 
            float temp = value;
            temp *= 100;
            criticalPercent_ = Mathf.Floor(temp) / 100;
        } 
    }


    /*
     :  関数↓
     */
    /// <summary>
    /// ダメージを与える(プレイヤーに対して)
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <remarks>property:ダメージ値は切り捨ての整数</remarks>
    protected virtual void TakeDamage(float damage)
    {
        hp_ -= damage;
    }
    /// <summary>
    /// 移動処理(もしかしたら引数無し版も作れるかも...?)
    /// </summary>
    /// <param name="input">0~1の入力値</param>
    /// <param name="character_controller">CharacterControllerコンポーネント</param>
    protected virtual void PlayerMove(Vector2 input, CharacterController character_controller)
    {
        Vector2 move_value = input;
        move_value *= moveSpeed_ * Time.deltaTime;
        character_controller.Move(move_value);
    }
}
