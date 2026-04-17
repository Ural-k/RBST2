using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    //ゲージUI
    public Image Gauge;

    //最大時間(秒)
    public float MaxTime;

    //自動開始をする
    public bool AutoStart = true;

    private float CurrentTime;

    private bool IsRunning;

    void Start()
    {
        //リセット範囲内
        ResetGauge();

        if(AutoStart)
        {
            //スタート範囲内
            StartGauge();
        }
    }

    void Update()
    {
        if (!IsRunning)
        {
            return;
        }

        CurrentTime -= Time.deltaTime;

        if (CurrentTime < 0f)
        {
            CurrentTime = 0f;

            //非表示にする
            IsRunning = false;
        }

        //0～1の範囲
        Gauge.fillAmount = CurrentTime / MaxTime;
    }

    //ゲージを開始する
    public void StartGauge()
    {
        //表示する
        IsRunning = true;
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        CurrentTime = MaxTime;
        Gauge.fillAmount = 1f;

        //非表示にする
        IsRunning = false;
    }
}