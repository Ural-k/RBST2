using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image[] icon_ = new Image[3];
    [SerializeField] private Image[] coolTimeIcon_ = new Image[3];

    private Sprite[] initalIcon_ = new Sprite[3];
    [SerializeField] private float[] memoryCoolDown_ = new float[3];

    private void Start()
    {
        for (int i = 0; i < icon_.Count(); ++i)
        {
            initalIcon_[i] = icon_[i].sprite;
        }
    }

    /// <summary>
    /// アイコンのSpriteを更新する
    /// </summary>
    public void SkillIconUpdate(int num, SkillData skill)
    {
        icon_[num].sprite = skill.nextSkill_.icon_;
        coolTimeIcon_[num].sprite = skill.nextSkill_.icon_;
        for (int i = 0; i < memoryCoolDown_.Count(); ++i) memoryCoolDown_[i] = 0;
        for (int i = 0; i < 3; ++i)
        {
            if(i == num) memoryCoolDown_[i] = Mathf.Max(skill.cd_, skill.gcd_);
            else memoryCoolDown_[i] = skill.gcd_;
        }
    }

    public void ResetIcon()
    {
        icon_[0].sprite = initalIcon_[0];
        icon_[1].sprite = initalIcon_[1];
        icon_[2].sprite = initalIcon_[2];
        coolTimeIcon_[0].sprite = initalIcon_[0];
        coolTimeIcon_[1].sprite = initalIcon_[1];
        coolTimeIcon_[2].sprite = initalIcon_[2];
    }

    public void IconCoolDown(float[] cd, float gcd)
    {
        for(int i = 0; i < cd.Count(); ++i)
        {
            icon_[i].fillAmount = 1 - (Mathf.Max(cd[i], gcd) / memoryCoolDown_[i]);
        }
    }
}
