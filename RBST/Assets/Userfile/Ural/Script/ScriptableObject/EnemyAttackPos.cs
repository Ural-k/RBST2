using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttackPos")]
public class EnemyAttackPos : ScriptableObject
{
    public Vector2[] attackPos;
    public float[] scale;
    public AOECollect[] aoeCollect;
}
