using UnityEngine;
using UnityEngine.UI;

public class TitleManager_Beta : MonoBehaviour
{
    [SerializeField] Image black_;
    float timer_;

    private void Start()
    {
        timer_ = 1.5f;
    }

    private void Update()
    {
        black_.color = new Color(0, 0, 0, timer_);
        if(timer_ == 0) black_.gameObject.SetActive(false);
        timer_ = Mathf.Max(timer_ - Time.deltaTime / 5, 0);
    }
}
