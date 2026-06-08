using UnityEngine;
using UnityEngine.InputSystem;

public class TestMulti : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が入室");
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が退室");
    }
}
