using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStatusData", menuName = "ScriptableObjects/Status/PlayerStatus")]
public class PlayerBaseStatus : ScriptableObject
{
    [SerializeField] public float move_speed_;
}
