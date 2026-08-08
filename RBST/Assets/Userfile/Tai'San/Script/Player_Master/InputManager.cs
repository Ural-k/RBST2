using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance_;
    public static InputManager Instance { get { return instance_; } }

    private void Awake() => instance_ = this;

    public event Action<InputAction.CallbackContext> onMove_;
    public event Action<InputAction.CallbackContext> onSkill1_;
    public event Action<InputAction.CallbackContext> onSkill2_;
    public event Action<InputAction.CallbackContext> onSkill3_;
    public event Action<InputAction.CallbackContext> onInteract_;

    //メモ：ネット対応させるとき、ここにIsMine入れるといいかも
    public void OnMove(InputAction.CallbackContext context)     => onMove_?.Invoke(context);
    public void OnSkill1(InputAction.CallbackContext context)   => onSkill1_?.Invoke(context);
    public void OnSkill2(InputAction.CallbackContext context)   => onSkill2_?.Invoke(context);
    public void OnSkill3(InputAction.CallbackContext context)   => onSkill3_?.Invoke(context);
    public void OnInteract(InputAction.CallbackContext context) => onInteract_?.Invoke(context);
}
