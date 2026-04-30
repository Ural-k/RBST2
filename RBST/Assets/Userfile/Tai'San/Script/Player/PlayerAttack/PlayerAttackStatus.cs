using UnityEngine;

/// <summary>
/// PlayerAttack自体の動き
/// </summary>
public class PlayerAttackStatus : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}
