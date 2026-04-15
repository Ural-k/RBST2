using UnityEngine;
using UnityEngine.UI;

public class CircularGauge : MonoBehaviour
{
    [SerializeField] private Image Gauge;

    [SerializeField, Range(0f, 1f)] private float FillAmount = 1f;

    void Start()
    {
        Gauge.gameObject.SetActive(true);
    }
    void Update()
    {
        FillAmount = Mathf.PingPong(Time.time * 1, 1);

        if (Gauge != null)
        {
            Gauge.fillAmount = FillAmount;
            Gauge.fillAmount = 1;
        }
    }

    public void SetGauge(float value)
    {
        Gauge.fillAmount = Mathf.Clamp01(value);
    }
}