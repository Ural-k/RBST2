using System;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance { get; set; }

    /// <summary>
    /// ƒXƒLƒ‹‚ª”­“®‚µ‚½Žž
    /// </summary>
    public event Action<SkillData> OnSkill_;
}
