using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaPlayerIcon : MonoBehaviour
{
    [SerializeField] private Slider slider1_;
    [SerializeField] private Slider slider2_;
    [SerializeField] private Slider slider3_;
    [SerializeField] private Text text1_;
    [SerializeField] private Text text2_;
    [SerializeField] private Text text3_;
    [SerializeField] private List<RectTransform> skill1_;
    [SerializeField] private List<RectTransform> skill2_;
    [SerializeField] private List<RectTransform> skill3_;

    private void Start()
    {
        ActiveIconAllReset();
    }

    /// <summary>
    /// スキル１のクールダウン
    /// </summary>
    /// <param name="gcd"></param>
    /// <param name="cd"></param>
    public void SetCoolTime1(float gcd, float cd)
    {
        slider1_.value = Mathf.Max(gcd, cd);
    }
    /// <summary>
    /// スキル２のクールダウン
    /// </summary>
    /// <param name="gcd"></param>
    /// <param name="cd"></param>
    public void SetCoolTime2(float gcd, float cd)
    {
        slider2_.value = Mathf.Max(gcd, cd);
    }
    /// <summary>
    /// スキル３のクールダウン
    /// </summary>
    /// <param name="gcd"></param>
    /// <param name="cd"></param>
    public void SetCoolTime3(float gcd, float cd)
    {
        slider3_.value = Mathf.Max(gcd, cd);
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

    /// <summary>
    /// スキル１のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon1(int num)
    {
        foreach (RectTransform r in skill1_) { r.localScale = Vector3.one * 0.7f; }
        skill1_[num].localScale = Vector3.one;
    }
    /// <summary>
    /// スキル２のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon2(int num)
    {
        foreach (RectTransform r in skill2_) { r.localScale = Vector3.one * 0.7f; }
        skill2_[num].localScale = Vector3.one;
    }
    /// <summary>
    /// スキル３のアクティブアイコン変更
    /// </summary>
    /// <param name="num">アクティブになるコンボ数</param>
    public void ActiveIcon3(int num)
    {
        foreach (RectTransform r in skill3_) { r.localScale = Vector3.one * 0.7f; }
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
