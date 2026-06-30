using System.Collections;
using UnityEngine;

public class TeleportMove : MonoBehaviour, IEnemyMove
{
    public EnemyMoveCollect moveCollect
    {
        get { return EnemyMoveCollect.teleport; }
    }

    public IEnumerator EnemyMoveColutine(Transform enemy, EnemyMoveStract emStruct)
    {
        SpriteRenderer spriteRenderer = enemy.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        float fadeTime = Mathf.Max(emStruct.fadeTime, 0.01f);

        if (spriteRenderer != null)
        {
            yield return Fade(spriteRenderer, 1f, 0f, fadeTime);
        }

        enemy.position = emStruct.pos;

        if (spriteRenderer != null)
        {
            yield return Fade(spriteRenderer, 0f, 1f, fadeTime);
        }
    }

    private IEnumerator Fade(
        SpriteRenderer spriteRenderer,
        float startAlpha,
        float endAlpha,
        float fadeTime
    )
    {
        float timer = 0f;

        Color color = spriteRenderer.color;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float t = timer / fadeTime;

            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = color;

            yield return null;
        }

        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}