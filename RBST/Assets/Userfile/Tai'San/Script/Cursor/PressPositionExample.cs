using UnityEngine;
using UnityEngine.InputSystem;

public class PressPositionExample : MonoBehaviour
{
    // ボタンの押下状態
    [SerializeField] private InputActionProperty _pressAction;

    private void Awake() => _pressAction.action.performed += PressAction;
    private void OnDestroy() => _pressAction.action.performed -= PressAction;
    private void OnEnable() => _pressAction.action.Enable();
    private void OnDisable() => _pressAction.action.Disable();

    private void PressAction(InputAction.CallbackContext context)
    {
        // Actionを使用すると、フォーカス復帰時に正しく位置取得できないことがあるので、
        // Pointerデバイスから直接位置を取得する
        var pointer = Pointer.current;
        if (pointer == null)
            return;

        // クリック位置を取得
        var position = pointer.position.ReadValue();

        // 取得座標をログ出力
        print($"Press position: {position}");
    }
}