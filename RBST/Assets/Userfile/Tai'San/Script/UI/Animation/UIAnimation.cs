using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private float loopTime_;
    [SerializeField] private float startScale_;
    [SerializeField] private float changeScale_;
    [SerializeField] private AnimationType animationType_;

    private enum AnimationType
    {
        None,
        ScalePingPong,
    }

    private void Update()
    {
        switch (animationType_)
        {
            case AnimationType.ScalePingPong:
                transform.localScale = Vector2.one * (Mathf.PingPong(Time.time / loopTime_, changeScale_) + startScale_);
                break;
            default: break;
        }
    }
}
