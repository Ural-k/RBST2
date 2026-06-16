using UnityEngine;

public enum FloatingTextType
{
    EnemyDamage,
    PlayerDamage,
    Heal,
}

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField] private DamageText damageTextPrefab_;
    [SerializeField] private Canvas canvas_;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(Vector3 worldPos, int value, FloatingTextType type)
    {
        var text = Instantiate(damageTextPrefab_, canvas_.transform);
        text.Setup(worldPos, value, type);
    }
}