using UnityEngine;

/// <summary>
/// ステータスの操作と移動
/// </summary>
/// <remarks>(プレイヤー基底クラス)</remarks>
public class PlayerBase : MonoBehaviour
{
    [SerializeField] JobStatus referenceJobStatus_;        //参照するジョブステータス
    [SerializeField] Job job_;                             //職業
    [SerializeField] JobStatusTemp status_;                 //ステータスまとめ

    protected string    jobName_;
    protected string    playerName_;
    protected float     speed_;
    protected float     attack_;
    protected float     defense_;
    protected float     gcd_;
    protected float     critical_;

    //動的ステータス変更(インスペクター操作用)
    //private void OnValidate() { status_ = referenceJobStatus_.GetJobStatus(job_); }

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
    public void TakeDamage(int damage)
    {
        status_.hp_ -= damage;//仮ダメージ計算
    }
    /// <summary>
    /// 移動処理(もしかしたら引数無し版も作れるかも...?)
    /// </summary>
    /// <param name="input">0~1の入力値</param>
    /// <param name="character_controller">CharacterControllerコンポーネント</param>
    protected virtual void PlayerMove(Vector2 input)
    {
        Vector2 move_value = input.normalized;
        move_value *= status_.moveSpeed_ * Time.deltaTime;
        transform.position += (Vector3)move_value;
    }

}