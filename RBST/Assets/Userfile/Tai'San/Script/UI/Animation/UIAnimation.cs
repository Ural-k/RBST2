using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private float loopTime_;
    [SerializeField] private float changeScale_;
    [SerializeField] private AnimationType animationType_;
    [SerializeField] private Color endColor_;
    private float startScale_;
    private Text text_;
    private Color startColor_;

    private enum AnimationType
    {
        None,
        ScalePingPong,
        TextColorPingPong,
        CenterLineAndText
    }
    private void Start()
    {
        switch (animationType_)
        {
            case AnimationType.ScalePingPong:
                transform.localScale = Vector2.one * (Mathf.PingPong(Time.time / loopTime_, changeScale_) + startScale_);
                break;
            case AnimationType.TextColorPingPong:
                text_.color = new Color();
                break;
            default: break;
        }
    }

    private void Update()
    {
        switch (animationType_)
        {
            case AnimationType.ScalePingPong:
                transform.localScale = Vector2.one * (Mathf.PingPong(Time.time / loopTime_, changeScale_) + startScale_);
                break;
            case AnimationType.TextColorPingPong:
                text_.color = new Color();
                break;
            default: break;
        }
    }

    public void OnCenterLineText()
    {

    }
}
