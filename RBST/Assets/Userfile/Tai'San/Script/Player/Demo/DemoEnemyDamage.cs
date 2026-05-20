using UnityEngine;

public class DemoEnemyDamage : MonoBehaviour,IEnemyDamageAble
{
    public void DamageAble(int damage)
    {
        Debug.Log($"{transform.parent.gameObject.name}に{damage}ダメージ！");
    }
}
