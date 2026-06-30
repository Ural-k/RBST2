using System.Collections;
using UnityEngine;

public class NormalMove : MonoBehaviour , IEnemyMove
{
    public EnemyMoveCollect moveCollect { get { return (EnemyMoveCollect.Normal); } }

    public IEnumerator EnemyMoveColutine(Transform enemy, EnemyMoveStract emStruct)
    {
        Vector2 startPos = enemy.position;
        Vector2 endPos = emStruct.pos;

        float timer = 0f;
        float moveTime = Mathf.Max(emStruct.moveTime, 0.01f);

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            float t = timer / moveTime;

            enemy.position = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        enemy.position = endPos;
    }
}
