using UnityEngine;

public class TargetCircle : MonoBehaviour,IToEnemyDamageAble
{
    public Transform circle_;
    public uint hp_;
    public void DamageAble(int damage)
    {
        hp_ -= (uint)damage;
        if(hp_ == 0)
        {
            Debug.Log($"{gameObject.name} Å® death");
        }
    }
}
