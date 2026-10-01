using UnityEngine;
using UnityEngine.EventSystems;

public class IconHelp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject helpImage_;

    private Player player_;

    private void Start()
    {
        player_ = transform.root.GetComponent<Player>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
