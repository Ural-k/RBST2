using UnityEngine;

public abstract class AttackBase : ScriptableObject
{
    [SerializeField] protected int power_;
    [SerializeField] protected float delay_;
    [SerializeField] protected TargetType targetType_;
    [SerializeField] protected PivotSet pivotSet_;
    [SerializeField] protected int targetNum_;

    public int GetPower { get { return power_; } }
    public TargetType GetTargetType { get { return targetType_; } }

    public enum PivotSet
    {
        Me,
        Near,
        Far,
        Random,
    }

    public abstract void Execute(ITargetCircle from);
}

public enum TargetType
{
    Enemy,
    Player,
}