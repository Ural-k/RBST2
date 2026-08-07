using System.Collections;
using UnityEngine;

/// <summary>
/// バフ・デバフ
/// </summary>
public class TestEffect : MonoBehaviour
{
    public static TestEffect Instance { get; }

    public TestEffect()
    {
        StartCoroutine(GameDeltaTime());
    }

    /// <summary>
    /// バフ更新
    /// </summary>
    IEnumerator GameDeltaTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            
        }
    }
}