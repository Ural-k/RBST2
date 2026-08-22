using UnityEngine;

/// <summary>
/// 表示するテキストの種類
/// </summary>
public enum FloatingTextType
{
    EnemyDamage,
    PlayerDamage,
    Heal,
}

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField] private DamageText damageTextPrefab_;　//生成するtextのprefab（重くなりそうならpoolにする予定
    [SerializeField] private Canvas canvas_;                //表示するcanvas

    private void Awake()
    {
        Instance = this;
        //シングルトン
        //if (Instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //else
        //{
        //    Instance = this;
        //}
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show(Vector3 worldPos, int value, FloatingTextType type)
    {
        var text = Instantiate(damageTextPrefab_, canvas_.transform);
        text.Setup(worldPos, value, type);
    }
}