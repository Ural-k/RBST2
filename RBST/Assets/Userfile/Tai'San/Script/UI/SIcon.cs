using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SkillIcon
/// </summary>
public class SIcon : MonoBehaviour
{
    [SerializeField] private GameObject help_;
    [SerializeField] private Image activeFrame_;

    private bool active_;

    public void OnClick()
    {
        Debug.Log("–³‚µ");
    }
    public void PointEnter() => help_.SetActive(true);
    public void PointExit() => help_.SetActive(false);
}
