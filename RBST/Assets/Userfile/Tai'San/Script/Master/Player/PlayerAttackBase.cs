using UnityEngine;

public abstract class PlayerAttackBase : ScriptableObject
{
    [SerializeField] protected float power_;
    [SerializeField] protected TargetType pivotType_;
    [SerializeField] protected PivotSet pivotSet_;
    [SerializeField, Range(1, 4)] protected float targetNum_;

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
        All
    }

    public abstract void Execute(Vector2 originPosition);
}
