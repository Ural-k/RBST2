using System;
using UnityEngine;

public class TempEffect : MonoBehaviour
{
    [Flags]
    enum AtkInfo
    {
        Thunder = 0,
        Attack = 1 << 0,
        Defence = 1 << 1,
    }
}
