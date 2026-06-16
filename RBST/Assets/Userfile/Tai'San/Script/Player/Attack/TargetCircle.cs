using UnityEngine;

public class TargetCircle : MonoBehaviour, IToEnemyDamageAble
{
    public int hp_;
    public void DamageAble(int damage)
    {
        hp_ = Mathf.Max(hp_ - damage, 0);

        DamageTextManager.Instance.Show(
            transform.position,
            damage,
            FloatingTextType.EnemyDamage
        );

        if (hp_ == 0)
        {
            Destroy(gameObject);//‰¼
        }
    }

    //¦EnemyList‚ğRemove‚·‚éˆ—‚ª•K—v
}
