using UnityEngine;

public interface IEnemyMove 
{
    public void UpdatePoint(Vector2 v);

    public void StartWaiting();

    public void UpdateWaiting();

    public void EndWaiting();
}
