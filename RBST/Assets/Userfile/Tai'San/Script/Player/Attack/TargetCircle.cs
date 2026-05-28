using UnityEngine;

public class TargetCircle : MonoBehaviour, IToEnemyDamageAble
{
    public int hp_;
    public void DamageAble(int damage)
    {
        hp_ = Mathf.Max(hp_ - damage, 0);
        if (hp_ == 0)
        {
            Debug.Log($"{gameObject.name} ¨ death");
            Destroy(gameObject);//‰¼
        }
    }

    //¦EnemyList‚ğRemove‚·‚éˆ—‚ª•K—v
}
