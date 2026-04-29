using UnityEngine;

/// <summary>
/// 職業ごとの初期ステータスを参照
/// </summary>
[CreateAssetMenu(fileName = "JobsStatus", menuName = "ScriptableObjects/Status/JobsStatus")]
class JobStatus : ScriptableObject
{
    [SerializeField] PlayerStatus[] status_;
    [HideInInspector] public PlayerStatus GetJobStatus(Job job) { return status_[(int)job]; }
}

/// <summary>
/// プレイヤーのステータス項目
/// </summary>
[System.Serializable]
public struct PlayerStatus
{                                     //ゲーム内表記例↓↓
    public string jobName_;           //(例)ナイト
    public string playerName_;        //(例)taisan☆(←ホストに☆などを入れる予定)

    public float moveSpeed_;          //MOV

    public int   level_;              //Lv
    public float exp_;                //EXP

    public float hp_;                 //HP
    public float attack_;             //ATK (←物理ダメージ・魔法ダメージで分けるか別で作るか未定)
    public float defense_;            //DEF
    public float criticalPercent_;    //CRT
}

public enum Job
{
    FiveMan,
    DemoTank,
    DemoHealer,
    DemoDPS,
    Count
}