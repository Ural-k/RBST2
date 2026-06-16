using UnityEngine;
using UnityEngine.UI;

public class EnemyHpSlider : MonoBehaviour
{
    [SerializeField] private Slider hpSlider_;
    [SerializeField] private int enemyIndex_;

    private void Awake()
    {
        if (hpSlider_ == null)
        {
            hpSlider_ = GetComponent<Slider>();
        }
    }

    private void Start()
    {
        hpSlider_.minValue = 0f;
        hpSlider_.maxValue = 1f;
        hpSlider_.value = 1f;
    }

    private void LateUpdate()
    {
        EnemyControl enemy = EnemyManager.GetEnemy(enemyIndex_);

        if (enemy == null)
        {
            hpSlider_.gameObject.SetActive(false);
            return;
        }

        hpSlider_.gameObject.SetActive(true);

        float hpRate = 0f;
        if (enemy.MaxHP > 0)
        {
            hpRate = (float)enemy.HP / enemy.MaxHP;
        }

        hpSlider_.value = Mathf.Clamp01(hpRate);
    }
}