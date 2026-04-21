using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    //ゲージUI
    public Image Gauge;

    //仮のUI
    public Image Kari;

    //半透明UI
    public Image Kuro;

    //数字Text
    public Text Suuji;

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
        //非表示にする
        Gauge.gameObject.SetActive(false);
        Kuro.gameObject.SetActive(false);
        Suuji.gameObject.SetActive(false);

        //表示する
        Kari.gameObject.SetActive(true);
    }

    void Update()
    {
        //特定のキーを押したら
        if(Input.GetKeyDown(KeyCode.Q))
        {
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

        //ゲージのUIが減るのと同時に数字のTextも減っていく
        Suuji.text = Mathf.CeilToInt(CurrentTime).ToString();

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

        //表示する
        Gauge.gameObject.SetActive(true);
        Kuro.gameObject.SetActive(true);
        Suuji.gameObject.SetActive(true);
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        //Maxに戻す
        Gauge.fillAmount = 1f;

        //停止する
        IsRunning = false;

        //非表示にする
        Kuro.gameObject.SetActive(false);
        Suuji.gameObject.SetActive(false);

        //表示する
        Kari.gameObject.SetActive(true);
    }
}