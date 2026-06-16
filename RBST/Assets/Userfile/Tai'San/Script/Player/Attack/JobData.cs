using UnityEngine;

/*
 :  <ジョブの実装の仕方>
 :  1.Assets/Userfile/Tai'San/Resources/Job の中で右クリ。
 :  2.Create/ScriptableObjects/Player/JobDataをクリック。
 :  3.名前を他のアセットに合わせる形で書き換える
 :  4.ジョブの名前を書き、スキルをそれぞれ設定する
 :
 :  ※ジョブ固有のスキルを実装したい場合
 :  5.JobSystemの中にpublic class ジョブ名(英) : IJobSystemを実装
 :  6.インターフェースに沿って固有のスキルを作る。オブジェクトを追加する際などにJobSystemに変数を追加してもいい
 */

/// <summary>
/// スキルの取得
/// </summary>
[CreateAssetMenu(fileName = "99_JobName(UnNamed)", menuName = "ScriptableObjects/Player/JobData")]
public class JobData : ScriptableObject
{
    [SerializeField] private string jobName_;
    [SerializeField] private SkillData[] skill1_;
    [SerializeField] private SkillData[] skill2_;
    [SerializeField] private SkillData[] skill3_;

    //ジョブ取得
    private static JobData GetJobsData(int number) { return Resources.LoadAll<JobData>("Job")[number]; }

    /// <summary>
    /// スキル取得
    /// </summary>
    /// <param name="jobNumber">ジョブナンバー</param>
    public static JobData GetJobSkill(int jobNumber) { return GetJobsData(jobNumber); }

    //発動スキル取得
    public SkillData[] GetSkill1() { return skill1_; }
    public SkillData[] GetSkill2() { return skill2_; }
    public SkillData[] GetSkill3() { return skill3_; }

    /// <summary>
    /// ジョブの名前
    /// </summary>
    public string GetJobName { get { return jobName_; } }
}

/*
 :  攻撃対象
 */
public enum TargetType
{
    Enemy,
    Player,
    Natural,
    Null
}

/*
 :  スキルタイプ
 */
public enum SkillType
{
    Single,
    Circle,
    Square,
}