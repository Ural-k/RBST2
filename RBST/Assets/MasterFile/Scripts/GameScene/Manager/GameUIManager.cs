using UnityEngine;
using UnityEngine.Rendering;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField] private Canvas resultUI_;
    [SerializeField] private Canvas playUI_;


    private void Awake()
    {
        resultUI_.gameObject.SetActive(false);
        playUI_.gameObject.SetActive(false);
    }
    
    public void SetPlayUI()
    {
        playUI_.gameObject.SetActive(true);
    }

    public void HidePlayUI()
    {
        playUI_.gameObject.SetActive(false);
    } 

    public void SetResultUI()
    {
        resultUI_.gameObject.SetActive(true);
    }

    public void HideResultUI()
    {
        resultUI_.gameObject.SetActive(false);
    }
}