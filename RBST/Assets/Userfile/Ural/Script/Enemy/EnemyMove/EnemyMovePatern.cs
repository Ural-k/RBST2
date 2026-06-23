using UnityEngine;

public abstract class EnemyMovePattern : ScriptableObject
{
    public abstract EnemyMoveRunner CreateRunner(Transform enemy);
}

public abstract class EnemyMoveRunner
{
    protected Transform enemy_;

    protected EnemyMoveRunner(Transform enemy)
    {
        enemy_ = enemy;
    }

    public virtual void Enter() { }
    public abstract void Tick();
    public virtual void Exit() { }
}