using UnityEngine;

public class Effect : MonoBehaviour
{
    public EffectData Data;
    public float RemainingTime;
    public int StackCount = 1;
    public TestPlayerBase Target;

    public bool IsExpired => RemainingTime <= 0;
}
