using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ステータスの操作と移動
/// </summary>
/// <remarks>(プレイヤー基底クラス)</remarks>
public class PlayerBase : MonoBehaviour
{
    [SerializeField] JobStatus referenceJobStatus_;        //参照するジョブステータス
    [SerializeField] Job job_;                             //職業
    [SerializeField] PlayerStatus status_;                 //ステータスまとめ

    //動的ステータス変更(インスペクター操作用)
    private void OnValidate() { status_ = referenceJobStatus_.GetJobStatus(job_); }

    /// <summary>
    /// 職業名
    /// </summary>
    /// <remarks>property:ReadOnly</remarks>
    protected string JobName { get { return status_.jobName_; } }
    /// <summary>
    /// 移動速度
    /// </summary>
    /// <remarks>property:0~x</remarks>
    protected float MoveSpeed { get { return status_.moveSpeed_; } set { status_.moveSpeed_ = value <= 0 ? 0 : value; } }
    /// <summary>
    /// 攻撃力
    /// </summary>
    /// <remarks>property:0~x</remarks>
    protected float Attack { get { return status_.attack_; } set { status_.attack_ = value <= 0 ? 0 : value; } }
    /// <summary>
    /// 防御力
    /// </summary>
    ///<remarks>property:nothing</remarks>
    protected float Defence { get { return status_.defense_; } set { status_.defense_ = value; } }
    /// <summary>
    /// クリティカル率
    /// </summary>
    /// <remarks>property:0.00~1.00</remarks>
    protected float CriticalPercent
    { 
        get { return status_.criticalPercent_; } 
        set
        {
            //第二引数切り捨て
            float temp = value;
            temp *= 100;
            status_.criticalPercent_ = Mathf.Floor(temp) / 100;
        } 
    }


    /*
     :  関数↓↓
     */
    
    /// <summary>
    /// ダメージを与える(プレイヤーに対して)
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <remarks>property:ダメージ値は切り捨ての整数</remarks>
    protected virtual void TakeDamage(float damage)
    {
        status_.hp_ -= damage;
    }
    /// <summary>
    /// 移動処理(もしかしたら引数無し版も作れるかも...?)
    /// </summary>
    /// <param name="input">0~1の入力値</param>
    /// <param name="character_controller">CharacterControllerコンポーネント</param>
    protected virtual void PlayerMove(Vector2 input, CharacterController character_controller)
    {
        Vector2 move_value = input.normalized;
        move_value *= status_.moveSpeed_ * Time.deltaTime;
        character_controller.Move(move_value);
    }
}

[System.Serializable]
public struct PlayerStatus
{                                                      //ゲーム内表記↓↓
    [SerializeField] public string jobName_;           //(例)ナイト※編集不可
    [SerializeField] public float moveSpeed_;          //MOV
    [SerializeField] public float hp_;                 //HP
    [SerializeField] public float attack_;             //ATK
    [SerializeField] public float defense_;            //DEF
    [SerializeField] public float criticalPercent_;    //CRT
}

public enum Job
{
    FiveMan,
    DemoTank,
    DemoHealer,
    DemoDPS,
    Count
}

/// <summary>
/// ジョブステータスのテンプレート値を取得
/// </summary>
[CreateAssetMenu(fileName = "JobsStatus", menuName = "ScriptableObjects/Status/JobsStatus")]
class JobStatus : ScriptableObject
{
    [SerializeField] PlayerStatus[] status_;
    [HideInInspector] public PlayerStatus GetJobStatus(Job job) { return status_[(int)job]; }
}
