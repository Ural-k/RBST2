using UnityEngine;

public class Enemy : MonoBehaviour, ITargetCircle
{
    [SerializeField] private Parameter parameter_;
    [SerializeField] private float targetRadiuse_;

    //ターゲットサークル
    Vector2 ITargetCircle.GetPosition => transform.position;
    public Parameter Parameter => parameter_;
    public float Radius => targetRadiuse_;
    public Effect Effect { get; set; }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Died()
    {
        GameObjectManager.Instance.DestroyEnemy(this);
        Destroy(this);
    }

    void ITargetCircle.TakeDamage(int damage, ITargetCircle from)
    {
        ShowFloatingText(damage, FloatingTextType.EnemyDamage);
        parameter_.hp_ -= damage;
    }

    void ITargetCircle.TakeHeal(int point, ITargetCircle from)
    {
        ShowFloatingText(point, FloatingTextType.Heal);
        parameter_.hp_ += point;
    }

    /// <summary>
    /// ダメージテキストの呼び出し
    /// </summary>
    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }


    private void Start()
    {
        Effect = new(this);
        //EnemyManager.AddEnemy(this);
    }
}
