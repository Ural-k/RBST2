using UnityEngine;

[CreateAssetMenu(fileName = "AttackParticles", menuName = "ScriptableObjects/Player/AttackParticles")]
public class AttackParticles : ScriptableObject
{
    [SerializeField] public GameObject fire_;
    [SerializeField] public GameObject meteor_;
}
