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
 :  スキル設定
 */
[System.Serializable]
public struct SkillData
{                                       //役割[参照する強化値(変動する値)]
    /* 追加する値メモ
     :  ・offset,rotate
     :  ・inputで向き指定
     :  ・マウス
     */

    //固定ステータス
    [Tooltip("表示名")] public string name_;                                   //△
    [Tooltip("演出プレファブ")] public GameObject particle_;                     //o
    [Tooltip("攻撃対象")] public TargetType targetType_;                        //o
    //[Tooltip("次回の攻撃")]                    public SkillName combo_;       //o
    //public Buff buff_;                  //付与するバフx
    //public DeBuff deBuff_;              //付与するデバフx
    [Tooltip("近い攻撃対象を中心に")] public bool toTarget_;                      //o

    //範囲
    [Tooltip("矩形比率")] public Vector2 aspect_;                               //o
    [Tooltip("半径")] public float radius_;                                     //o

    //攻撃パラメータ
    [Tooltip("威力値")] public int power_;                                       //△
    [Tooltip("GCD")] public float gcd_;                                         //o
    [Tooltip("CD")] public float cd_;                                           //o
    //[Tooltip("発動タイミング")]                 public float diray_;            //x
    [Tooltip("発動後の動き")] public MotionInfo motion_;                          //o
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

[System.Serializable]
public struct MotionInfo
{
    public float time_;
    public float speed_;
    public bool jumpOn_;
    public AnimationCurve jumpOrbit_;
}

