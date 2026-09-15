using UnityEngine;
using UnityEngine.UI;

public class EffectIcon : MonoBehaviour
{
    [SerializeField] private Image icon_;
    [SerializeField] private Image[] arrows_ = new Image[5];

    public EffectIcon()
    {

    }

    public void Initialize(Vector2 pos, EffectController effect)
    {
        transform.localPosition = pos;
        foreach (var a in arrows_) { a.color = new Color(1, 1, 1, 0); }
        Active(effect);
    }

    /// <summary>
    /// バフUIの操作
    /// </summary>
    /// <param name="visible">有効にするかどうか</param>
    /// <param name="effect">操作するバフ・デバフ(Trueの時のみ必須)</param>
    public void Active(EffectController effect)
    {
        icon_.sprite = effect.data_.icon_;//アイコン
        foreach (var a in arrows_) { a.sprite = effect.data_.arrow_; }//矢印
        icon_.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < effect.stackCount_ - 1; ++i)
        {
            arrows_[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void Passive()
    {
        icon_.color = new Color(1, 1, 1, 0);
        foreach(var a in arrows_) { a.color = new Color(1, 1, 1, 0); }
    }
}