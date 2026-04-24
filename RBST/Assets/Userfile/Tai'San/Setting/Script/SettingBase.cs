using UnityEngine;
using UnityEngine.InputSystem;

/*
 :  基底クラス
 */
public class SettingBase : MonoBehaviour
{
    /*
     :  General
    */
    [SerializeField, Range(0, 100)] int bgm_;
    [SerializeField, Range(0, 100)] int se_;
    [SerializeField] bool cameraShake_;         //実装不確定
    [SerializeField] bool partyEffect_;
    [SerializeField] bool myEffect_;

    /*
     :  Player
     */
    [SerializeField,Range(0,10)] int mouseSensitivity_;

    /*
     :  KeyBind
     */
    [SerializeField] InputAction keyAction_;
}

/// <summary>
/// キーバインド設定用の変数と機能たち
/// </summary>
[System.Serializable]
public class KeyBind
{
    [SerializeField] InputActionAsset inputAssetPlayer_;
    InputAction primary_;
    InputAction secondary_;
    InputAction special_;
    InputAction defense_;

    KeyBind()
    {
        //primary_ =
    }

    /// <summary>
    /// キーバインド設定をリセット
    /// </summary>
    void InputActionsReset()
    {
        
    }
}
