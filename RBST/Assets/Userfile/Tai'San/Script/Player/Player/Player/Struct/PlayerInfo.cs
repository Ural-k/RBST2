using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの状態
/// </summary>
/// <remarks>GCD/CD・コンボ・職業etc</remarks>
[System.Serializable]
public struct PlayerInfo
{
    [SerializeField] public Parameter parameter_;

    [HideInInspector] public float          gcd_;//o
    [HideInInspector] public InputSkillInfo skill1_;//↓まとめる
    [HideInInspector] public InputSkillInfo skill2_;//
    [HideInInspector] public InputSkillInfo skill3_;//--------
    [HideInInspector] public float          activeCombo_;//x
    [HideInInspector] public JobData        jobData_;//要らない
    [HideInInspector] public InputAction    inputAxis_;//o
    [HideInInspector] public Vector2        lastFace_;//要らない
    [HideInInspector] public int            filip_;//x
    [HideInInspector] public float          downTime_;//x
    [HideInInspector] public int            lastInput_;//要らない
    [HideInInspector] public SpriteRenderer avatar_;//x

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        inputAxis_      = InputSystem.actions.FindAction("Move");
        avatar_         = GameObject.Find("Avatar").GetComponent<SpriteRenderer>();
        parameter_.HP   = parameter_.maxHp_;
        lastFace_       = Vector2.right;
        jobData_        = JobData.GetJobSkill(parameter_.jobNumber_);
    }

    /// <summary>
    /// ジョブごとのパラメータリセット(まだ仮)
    /// </summary>
    public void ParameterReset()
    {
        parameter_      = new Parameter { HP = parameter_.maxHp_ };
        skill1_         = new InputSkillInfo();
        skill2_         = new InputSkillInfo();
        skill3_         = new InputSkillInfo();
        gcd_            = 0;
        lastInput_      = 0;
        activeCombo_    = 0;
        downTime_       = 0;
        lastFace_       = Vector2.right;
        jobData_        = JobData.GetJobSkill(parameter_.jobNumber_);
    }

    /// <summary>
    /// ジョブ変更
    /// </summary>
    /// <remarks>同時にコンボとCDのリセットが行われる</remarks>
    public void JobChange(int jobNumber)
    {
        if (parameter_.jobNumber_ == jobNumber) return;
        parameter_.jobNumber_   = jobNumber;
        skill1_                 = new InputSkillInfo { cd_ = 0, nowCombo_ = 0 };
        skill2_                 = new InputSkillInfo { cd_ = 0, nowCombo_ = 0 };
        skill3_                 = new InputSkillInfo { cd_ = 0, nowCombo_ = 0 };
        jobData_                = JobData.GetJobSkill(jobNumber);
    }

    /// <summary>
    /// プレイヤーをposの方向へ向かせる
    /// </summary>
    /// <param name="pos">向かせる方向</param>
    /// <param name="me">自身のTransform</param>
    /// <param name="horizontal"></param>
    /// <returns>向かせた方向</returns>
    public Vector2 LookAt(Vector3 pos, Transform me, bool horizontal = false)
    {
        if (me.position != pos)
        {
            lastFace_                   = Vector2.Normalize(pos - me.position);
            if (horizontal) lastFace_   *= Vector2.right;
            filip_                      = lastFace_.x == 0 ? filip_ : (int)Mathf.Sign(lastFace_.x);
            if (filip_ > 0)             avatar_.flipX = true;
            else if (filip_ < 0)        avatar_.flipX = false;
        }
        return lastFace_;
    }
}