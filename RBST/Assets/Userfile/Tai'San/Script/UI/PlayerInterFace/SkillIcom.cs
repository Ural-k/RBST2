using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillIcom : MonoBehaviour
{
    //プレイヤのconstをグローバルにする
    const int INPUT_SKILL_ONE = 1;
    const int INPUT_SKILL_TWO = 2;
    const int INPUT_SKILL_THREE = 3;

    [SerializeField] private Transform line_;
    [SerializeField] private List<GameObject> icon1_ = new List<GameObject>();
    [SerializeField] private List<GameObject> icon2_ = new List<GameObject>();
    [SerializeField] private List<GameObject> icon3_ = new List<GameObject>();
    [SerializeField] private Image frame1_, frame2_, frame3_;
    int nowCombo1_, nowCombo2_, nowCombo3_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeIcon(INPUT_SKILL_TWO, nowCombo2_ + 1);
        }
    }

    /// <summary>
    /// アイコン表示
    /// </summary>
    /// <param name="input">入力スキル</param>
    /// <param name="nextcombo">次のコンボ</param>
    public void ChangeIcon(int input,int nextcombo)
    {
        switch (input)
        {
            case INPUT_SKILL_ONE    : ChangeIcon1(nextcombo); break;
            case INPUT_SKILL_TWO    : ChangeIcon2(nextcombo); break;
            case INPUT_SKILL_THREE  : ChangeIcon2(nextcombo); break;
        }
    }

    private void ChangeIcon1(int next)
    {
        icon1_[nowCombo1_].SetActive(false);
        if (next > icon1_.Count) nowCombo1_ = 0;
        else nowCombo1_ = next;
        icon1_[nowCombo1_].SetActive(true);
    }

    private void ChangeIcon2(int next)
    {
        icon2_[nowCombo2_].SetActive(false);
        if (next == icon2_.Count) nowCombo2_ = 0;
        else nowCombo2_ = next;
        icon2_[nowCombo2_].SetActive(true);
    }

}
