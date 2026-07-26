using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaPlayerIcon : MonoBehaviour
{
    [SerializeField] private Text text1_;
    [SerializeField] private Text text2_;
    [SerializeField] private Text text3_;
    [SerializeField] private List<RectTransform> skill1_;
    [SerializeField] private List<RectTransform> skill2_;
    [SerializeField] private List<RectTransform> skill3_;
    [SerializeField] private List<Image> coolTimeHelp1_;
    [SerializeField] private List<Image> coolTimeHelp2_;
    [SerializeField] private List<Image> coolTimeHelp3_;
    float helpTimer1_, helpTimer2_, helpTimer3_;
    float helpTime1_, helpTime2_, helpTime3_;
    float gcdTime_, gcdTimer_;

    private void Start()
    {
        ActiveIconAllReset();
    }

    private void Update()
    {
        foreach (var help in coolTimeHelp1_) 
        { help.fillAmount = Mathf.Max(NanIsZero(helpTimer1_ / helpTime1_), NanIsZero(gcdTimer_ / gcdTime_), 0); helpTimer1_ = Mathf.Max(helpTimer1_ - Time.deltaTime, 0); }
        foreach (var help in coolTimeHelp2_) 
        { help.fillAmount = Mathf.Max(NanIsZero(helpTimer2_ / helpTime2_), NanIsZero(gcdTimer_ / gcdTime_), 0); helpTimer2_ = Mathf.Max(helpTimer2_ - Time.deltaTime, 0); }
        foreach (var help in coolTimeHelp3_) 
        { help.fillAmount = Mathf.Max(NanIsZero(helpTimer3_ / helpTime3_), NanIsZero(gcdTimer_ / gcdTime_), 0); helpTimer3_ = Mathf.Max(helpTimer3_ - Time.deltaTime, 0); }
        gcdTimer_ = Mathf.Max(gcdTimer_ - Time.deltaTime, 0);
        //Debug.Log($"{helpTimer3_} / {helpTime3_} = {helpTimer3_ / helpTime3_} : {gcdTimer_} / {gcdTime_} = {gcdTimer_ / gcdTime_}");
    }

    float NanIsZero(float num)
    {
        if (float.IsNaN(num)) return 0f;
        else return num;
    }

    public void SetText1(string st)
    {
        text1_.text = st;
    }
    public void SetText2(string st)
    {
        text2_.text = st;
    }
    public void SetText3(string st)
    {
        text3_.text = st;
    }

    public void SetGCD(float second)
    {
        gcdTime_ = second;
        gcdTimer_ = second;
    }

    /// <summary>
    /// スキル１のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon1(int num, float cd)
    {
        foreach (RectTransform r in skill1_) { r.localScale = Vector3.one * 0.7f; }
        if(cd != 0) helpTime1_ = cd; helpTimer1_ = cd;
        skill1_[num].localScale = Vector3.one;
    }
    /// <summary>
    /// スキル２のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon2(int num, float cd)
    {
        foreach (RectTransform r in skill2_) { r.localScale = Vector3.one * 0.7f; }
        if (cd != 0) helpTime2_ = cd; helpTimer2_ = cd;
        skill2_[num].localScale = Vector3.one;
    }
    /// <summary>
    /// スキル３のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon3(int num, float cd)
    {
        foreach (RectTransform r in skill3_) { r.localScale = Vector3.one * 0.7f; }
        if (cd != 0) helpTime3_ = cd; helpTimer3_ = cd;
        skill3_[num].localScale = Vector3.one;
    }

    public void ActiveIconAllReset()
    {
        foreach (RectTransform r in skill1_) { r.localScale = Vector3.one * 0.7f; }
        foreach (RectTransform r in skill2_) { r.localScale = Vector3.one * 0.7f; }
        foreach (RectTransform r in skill3_) { r.localScale = Vector3.one * 0.7f; }
        skill1_[0].localScale = Vector3.one;
        skill2_[0].localScale = Vector3.one;
        skill3_[0].localScale = Vector3.one;
    }
}
