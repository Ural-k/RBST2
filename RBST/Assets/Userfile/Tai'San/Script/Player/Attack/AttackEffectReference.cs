using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffectReference", menuName = "ScriptableObjects/Player/AttackEffectReference")]
public class AttackEffectReference : ScriptableObject
{
    [SerializeField] public GameObject fire_;
    [SerializeField] public GameObject meteor_;
}
