using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// １つのPlayerInputコンポーネントと併用
/// 設定は Send Messages
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager instance_;
    public static InputManager Instance { get { return instance_; } }

    private void Awake() => instance_ = this;

    public event Action<InputValue> OnMove_;
    public event Action<InputValue> OnSkill1_;
    public event Action<InputValue> OnSkill2_;
    public event Action<InputValue> OnSkill3_;
    public event Action<InputValue> OnInteract_;

    //メモ：ネット対応させるとき、ここにIsMine入れるといいかも
    private void OnMove(InputValue value)     => instance_.OnMove_?.Invoke(value);
    private void OnSkill1(InputValue value)   => instance_.OnSkill1_?.Invoke(value);
    private void OnSkill2(InputValue value)   => instance_.OnSkill2_?.Invoke(value);
    private void OnSkill3(InputValue value)   => instance_.OnSkill3_?.Invoke(value);
    private void OnInteract(InputValue value) => instance_.OnInteract_?.Invoke(value);
}
