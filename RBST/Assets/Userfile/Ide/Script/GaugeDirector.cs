using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    //ゲージUI
    public Image Gauge;

    public Image Kari;

    //最大時間(秒)
    public float MaxTime;

    //自動開始をする
    public bool AutoStart = true;

    //残り時間
    private float CurrentTime;

    //動作中のフラグ
    private bool IsRunning;

    void Start()
    {
        Kari.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Kari.gameObject.SetActive(false);

            //スタート関数
            StartGauge();
        }

        if (!IsRunning)
        {
            return;
        }

        //残り時間から減らす
        CurrentTime -= Time.deltaTime;

        //0以下にならない
        if (CurrentTime < 0f)
        {
            //0になったら
            CurrentTime = 0f;

            //停止する
            IsRunning = false;

            //リセット関数
            ResetGauge();
        }

        //0～1の範囲
        Gauge.fillAmount = CurrentTime / MaxTime;
    }

    //ゲージを開始する
    public void StartGauge()
    {
        //残り時間をMaxに戻す
        CurrentTime = MaxTime;

        //動作を開始する
        IsRunning = true;
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        //Maxに戻す
        Gauge.fillAmount = 1f;

        //停止する
        IsRunning = false;

        Kari.gameObject.SetActive(true);
    }
}