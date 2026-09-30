using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image[] icon_ = new Image[3];
    [SerializeField] private Image[] coolTimeIcon_ = new Image[3];

    private Sprite[] initalIcon_ = new Sprite[3];
    private float[] memoryCoolDown_ = new float[3];

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
    /// <param name="sprite"></param>
    public void SkillIconUpdate(SkillData skill1, SkillData skill2, SkillData skill3)
    {
        icon_[0].sprite = skill1.icon_;
        icon_[1].sprite = skill2.icon_;
        icon_[2].sprite = skill3.icon_;
        coolTimeIcon_[0].sprite = skill1.icon_;
        coolTimeIcon_[1].sprite = skill2.icon_;
        coolTimeIcon_[2].sprite = skill3.icon_;
        float maxGcd = Mathf.Max(skill1.gcd_, skill2.gcd_, skill3.gcd_);
        memoryCoolDown_[0] = Mathf.Max(skill1.cd_, maxGcd);
        memoryCoolDown_[1] = Mathf.Max(skill2.cd_, maxGcd);
        memoryCoolDown_[2] = Mathf.Max(skill3.cd_, maxGcd);
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
