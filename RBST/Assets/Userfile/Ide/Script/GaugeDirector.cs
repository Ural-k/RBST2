using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    //ゲージUI
    public Image gauge_;

    //仮のUI
    public Image kari_;

    //半透明UI
    public Image kuro_;

    //数字Text
    public Text suuji_;

    //最大時間(秒)
    public float maxTime_;

    //自動開始をする
    public bool autoStart = true;

    //残り時間
    private float currentTime_;

    //動作中のフラグ
    private bool isRunning_;

    void Start()
    {
        //ゲージと半透明と数字が非表示になる
        gauge_.gameObject.SetActive(false);
        kuro_.gameObject.SetActive(false);
        suuji_.gameObject.SetActive(false);

        //仮のImageを表示させる
        kari_.gameObject.SetActive(true);
    }

    void Update()
    {
        //特定のキーを押したら
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //スタート関数
            StartGauge();
        }

        if (!isRunning_)
        {
            return;
        }

        //残り時間から減らす
        currentTime_ -= Time.deltaTime;

        //0以下にならない
        if (currentTime_ < 0f)
        {
            //0になったら
            currentTime_ = 0f;

            //停止する
            isRunning_ = false;

            //リセット関数
            ResetGauge();
        }

        //ゲージのUIが減るのと同時に数字のTextも減っていく
        suuji_.text = Mathf.CeilToInt(currentTime_).ToString();

        //0～1の範囲
        gauge_.fillAmount = currentTime_ / maxTime_;
    }

    //ゲージを開始する
    public void StartGauge()
    {
        //残り時間をMaxに戻す
        currentTime_ = maxTime_;

        //動作を開始する
        isRunning_ = true;

        //ゲージと半透明と数字が表示させる
        gauge_.gameObject.SetActive(true);
        kuro_.gameObject.SetActive(true);
        suuji_.gameObject.SetActive(true);
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        //Maxに戻す
        gauge_.fillAmount = 1f;

        //停止する
        isRunning_ = false;

        //半透明と数字が非表示になる
        kuro_.gameObject.SetActive(false);
        suuji_.gameObject.SetActive(false);

        kari_.gameObject.SetActive(true);
    }
}