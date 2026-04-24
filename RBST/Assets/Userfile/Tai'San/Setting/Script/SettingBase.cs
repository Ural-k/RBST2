using UnityEngine;

public class SettingBase : MonoBehaviour
{
    [SerializeField, Range(0, 100)] int bgm_;
    [SerializeField, Range(0, 100)] int se_;
    [SerializeField] bool cameraShake_;
}

//public struct EffectOfAttack
//{
//    bool party_ = true;

//}
