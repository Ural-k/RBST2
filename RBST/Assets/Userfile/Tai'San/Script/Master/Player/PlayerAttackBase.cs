using UnityEngine;

public abstract class PlayerAttackBase : ScriptableObject
{
    protected float power_;

    public abstract void Attack();
}
