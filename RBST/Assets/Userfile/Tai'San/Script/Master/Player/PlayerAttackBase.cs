using UnityEngine;

public abstract class PlayerAttackBase : ScriptableObject
{
    [SerializeField] protected int power_;
    [SerializeField] protected TargetType targetType_;
    [SerializeField] protected PivotSet pivotSet_;
    [SerializeField, Range(1, 4)] protected int targetNum_;

    public enum TargetType
    {
        Enemy,
        Player,
    }
    public enum PivotSet
    {
        Me,
        Near,
        Far,
        Random,
    }

    public abstract void Execute(ITargetCircle from);
}
