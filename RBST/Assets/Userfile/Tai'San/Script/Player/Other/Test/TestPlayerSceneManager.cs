using UnityEngine;
using UnityEngine.UI;

public class TestPlayerSceneManager : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < 4; ++i) EnemyManager.AddEnemy(GameObject.Find($"Enemy{i + 1}").GetComponent<EnemyControl>());
    }
}
