using UnityEngine;

[CreateAssetMenu(fileName = "JobsStatus", menuName = "ScriptableObjects/Status/JobsStatus")]
class JobStatus : ScriptableObject
{
    [SerializeField]
    private PlayerStatusBase[] jobs_status = new PlayerStatusBase[(int)Job.Count];

    public PlayerStatusBase GetStatus(Job job) { return jobs_status[(int)job]; }
}

public enum Job
{
    FiveMan,
    DemoTank,
    DemoHealer,
    DemoDPS,
    Count
}

[System.Serializable]
class PlayerStatusBase
{
    [SerializeField] public string job_name_;
    [SerializeField] public float move_speed_;
    [SerializeField] public float attack_;
    [SerializeField] public float defense_;

    PlayerStatusBase()
    {
        job_name_ = new string("fiveman");
        move_speed_ = 5;
        attack_ = 5;
        defense_ = 5;
    }
}
