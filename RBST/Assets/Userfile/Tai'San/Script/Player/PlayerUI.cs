using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : PlayerBase
{
    [SerializeField] Text playerNameText_;

    private void Start()
    {
        playerNameText_.text = PlayerName;
    }
}
