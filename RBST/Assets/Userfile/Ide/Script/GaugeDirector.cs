using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    private Image Gauge;

    private float FillAmount = 1;

    void Update()
    {
        FillAmount = Mathf.PingPong(Time.time * 0.2f, 1f);

        if(Gauge != null)
        {
            Gauge.fillAmount = FillAmount;
        }
    }

    public void SetGauge(float value)
    {
        Gauge.fillAmount = Mathf.Clamp01(value);
    }
}