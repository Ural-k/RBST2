using UnityEngine;

public class Effect : MonoBehaviour
{
    public EffectData data_;
    public float remainingTime_;
    public int stackCount_ = 1;
    public ITestTargetCircle target_;

    public bool IsExpired => remainingTime_ <= 0;
}
