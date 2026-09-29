using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image[] icon_ = new Image[2];
    [SerializeField] private Image[] coolTimeIcon_ = new Image[2];

    /// <summary>
    /// アイコンのSpriteを更新する
    /// </summary>
    /// <param name="sprite"></param>
    public void IconChange(Sprite icon1, Sprite icon2, Sprite icon3)
    {
        icon_[0].sprite = icon1;
        icon_[1].sprite = icon2;
        icon_[2].sprite = icon3;
        coolTimeIcon_[0].sprite = icon1;
        coolTimeIcon_[1].sprite = icon2;
        coolTimeIcon_[2].sprite = icon3;
    }
}
