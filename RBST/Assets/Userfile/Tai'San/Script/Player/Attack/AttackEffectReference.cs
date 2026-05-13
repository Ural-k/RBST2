using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackReference", menuName = "ScriptableObjects/Player/AttackReference")]
public class AttackEffectReference : ScriptableObject
{
    [SerializeField] public GameObject fire_;
    [SerializeField] public GameObject meteor_;
}
