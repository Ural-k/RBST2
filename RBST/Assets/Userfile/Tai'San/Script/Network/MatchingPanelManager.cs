using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class MatchingPanelManager : MonoBehaviour
{
    private enum PanelType
    {
        RoomSelect,
        CreateRoom
    }

    [System.Serializable]
    struct MatchingPanel
    {
        public PanelType panelType_;
        public GameObject ui_;
    }
}
