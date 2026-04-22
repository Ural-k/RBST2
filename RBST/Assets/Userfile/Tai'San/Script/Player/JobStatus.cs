using UnityEngine;

[CreateAssetMenu(fileName = "JobsStatus", menuName = "ScriptableObjects/Status/JobsStatus")]
class JobStatus : ScriptableObject
{
    [SerializeField]
    private PlayerStatusBase[] jobsStatus = new PlayerStatusBase[(int)Job.Count];

    public PlayerStatusBase GetStatus(Job job) { return jobsStatus[(int)job]; }
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
/// プレイヤーステータス
/// </summary>
/// <remarks>ステータスの更新はこれを新しく宣言して使う</remarks>
[System.Serializable]
class PlayerStatusBase
{
    /*
     :  初期値
    */
    [SerializeField] private string jobName_;
    [SerializeField] private float moveSpeed_;
    [SerializeField] private float attack_;
    [SerializeField] private float defense_;
    [SerializeField] private float criticalPercent;

    //コンストラクタ
    PlayerStatusBase()
    {
        jobName_ = new string("fiveman");
        moveSpeed_ = 5;
        attack_ = 5;
        defense_ = 5;
    }

    /*
     :  ↓プロパティ↓
     */
    //ジョブ名
    public string JobName
    {
        get { return jobName_; }
        set { jobName_ = value; }
    }
    //移動速度
    public float MoveSpeed
    {
        get { return moveSpeed_; }
        set { moveSpeed_ = value; }
    }
    //攻撃力
    public float Attack
    {
        get { return attack_; }
        set { attack_ = value; }
    }
    //防御力
    public float Defense
    {
        get { return defense_; }
        set { defense_ = value; }
    }
    //クリティカル率(0%～99.9%)
    public float CriticalPercent
    {
        get { return criticalPercent; }
        set { criticalPercent = value; }
    }
}
