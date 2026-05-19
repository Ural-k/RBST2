using UnityEngine;

public class ActionDriver : MonoBehaviour,IPlayerDriver
{
    public TargetType targetType;
    public float radius_;
    public void DamageAble(int damage)
    {

    }
}

interface IPlayerDriver
{
    public void DamageAble(int damage);
}
