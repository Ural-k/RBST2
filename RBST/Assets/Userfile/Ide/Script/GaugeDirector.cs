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
        //リセット関数
        ResetGauge();

        if(AutoStart)
        {
            //スタート関数
            StartGauge();
        }
    }

    void Update()
    {
        if (!IsRunning)
        {
            return;
        }

        //CurrentTimeから減らす
        CurrentTime -= Time.deltaTime;

        if (CurrentTime < 0f)
        {
            //0になったら
            CurrentTime = 0f;

            //非表示になる
            IsRunning = false;
        }

        //0～1の範囲
        Gauge.fillAmount = CurrentTime / MaxTime;
    }

    //ゲージを開始する
    public void StartGauge()
    {
        //表示される
        IsRunning = true;
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        //CurrentTimeがMaxになる
        CurrentTime = MaxTime;

        //Maxに戻る
        Gauge.fillAmount = 1f;

        //非表示になる
        IsRunning = false;
    }
}