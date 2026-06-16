using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillFunction : MonoBehaviour
{
    PlayerInfo player_;

    private void Start()
    {
        TryGetComponent<Player>(out Player player);
        player_ = player.GetInfo;
    }

}
