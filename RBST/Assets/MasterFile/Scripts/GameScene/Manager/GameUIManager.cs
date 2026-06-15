using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public enum UIType
{
    CharacterSelect,
    Ready,
    Play,
    Result
}
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField] private List<UIData> uiDataList_;

    private Dictionary<UIType, GameObject> uiMap_;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        uiMap_ = new Dictionary<UIType, GameObject>();

        foreach (var data in uiDataList_)
        {
            uiMap_.Add(data.uiType, data.UIObject);
            data.UIObject.SetActive(false);
        }
    }

    public void Activate(UIType type)
    {
        uiMap_[type].SetActive(true);
    }

    public void Hide(UIType type)
    {
        uiMap_[type].SetActive(false);
    }
}