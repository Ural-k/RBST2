using UnityEngine;

public class ActionDriver : MonoBehaviour,IPlayerDriver
{
    public TargetType targetType;
    public void DamageAble(int damage)
    {

    }
}

interface IPlayerDriver
{
    public void DamageAble(int damage);
}
