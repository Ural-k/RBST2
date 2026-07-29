using UnityEngine;
using UnityEngine.UI;

public enum HpGaugeTargetType
{
    Enemy,
    Player
}

public class HpGauge : MonoBehaviour
{
    [SerializeField] private Slider hpSlider_;
    [SerializeField] private HpGaugeTargetType targetType_;
    [SerializeField] private int targetIndex_;
    [SerializeField] private bool hideWhenTargetMissing_ = true;

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
        switch (targetType_)
        {
            case HpGaugeTargetType.Enemy:
                UpdateEnemyHp();
                break;

            case HpGaugeTargetType.Player:
                UpdatePlayerHp();
                break;
        }
    }

    private void UpdateEnemyHp()
    {
        EnemyControl enemy = EnemyManager.GetEnemy(targetIndex_);

        if (enemy == null)
        {
            SetVisible(false);
            return;
        }

        SetVisible(true);
        SetHp(enemy.HP, enemy.MaxHP);
    }

    private void UpdatePlayerHp()
    {
        Player player = PlayerManager.GetPlayer(targetIndex_);

        if (player == null)
        {
            SetVisible(false);
            return;
        }

        
        SetVisible(true);
        SetHp(player.GetInfo.parameter_.HP, player.GetInfo.parameter_.maxHp_);
    }

    private void SetHp(float hp, float maxHp)
    {
        if (hpSlider_ == null) return;

        if (maxHp <= 0f)
        {
            hpSlider_.value = 0f;
            return;
        }

        hpSlider_.value = Mathf.Clamp01(hp / maxHp);
    }

    private void SetVisible(bool visible)
    {
        if (!hideWhenTargetMissing_) return;
        if (hpSlider_ == null) return;

        if (!visible) hpSlider_.value = 0;
        //hpSlider_.gameObject.SetActive(visible);
    }
}