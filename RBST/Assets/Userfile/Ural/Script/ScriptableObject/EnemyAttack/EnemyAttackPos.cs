using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttackPos")]
public class EnemyAttackStatus : ScriptableObject
{
    public EnemyAttackStract[] status;
    public int AttackCount;
}
