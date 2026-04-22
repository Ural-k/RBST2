using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    
    [SerializeField] private Image gaugeImage_;          //ゲージUI
    [SerializeField] private Image demoImage_;           //仮のUI
    [SerializeField] private Image backgroundImage_;     //半透明UI
    [SerializeField] private Text countText_;            //ゲージUIと一緒に表示するText
    [SerializeField] private float maxTime_;             //最大時間(秒)
    [SerializeField] private bool isAutoStart_ = true;   //自動開始をする
    private float currentTime_;                          //残り時間
    private bool isRunning_;                             //動作中のフラグ

    private void Start()
    {
        //ゲージUIと半透明UIと数字Textは非表示にする
        gaugeImage_.gameObject.SetActive(false);
        backgroundImage_.gameObject.SetActive(false);
        countText_.gameObject.SetActive(false);

        //仮のUIだけ表示にする
        demoImage_.gameObject.SetActive(true);
    }

    private void Update()
    {
        //特定のキー入力でゲージ開始
        if (Input.GetKeyDown(KeyCode.Q)) { StartGauge(); }

        //ゲージが動作していない場合は更新処理を行わない
        if (isRunning_ == false) { return; }

        //実時間ベースで残り時間を減らす
        currentTime_ -= Time.deltaTime;

        //残り時間が0を下回ったらカウントダウン終了処理を行う
        if (currentTime_ < 0f)
        {
            currentTime_ = 0f;
            isRunning_ = false;
            ResetGauge();
        }

        //ゲージのUIが減るのと同時に数字のTextも減っていく
        countText_.text = Mathf.CeilToInt(currentTime_).ToString();

        //0～1の範囲
        gaugeImage_.fillAmount = currentTime_ / maxTime_;
    }

    //ゲージを開始する
    public void StartGauge()
    {
        //ゲージ開始時に残り時間を最大値に戻し、カウントダウンを開始する
        currentTime_ = maxTime_;
        isRunning_ = true;

        //ゲージUIと半透明UIと数字Textと仮のUIを表示させる
        gaugeImage_.gameObject.SetActive(true);
        backgroundImage_.gameObject.SetActive(true);
        countText_.gameObject.SetActive(true);
        demoImage_.gameObject.SetActive(true);
    }

    //ゲージをリセットする
    public void ResetGauge()
    {
        //Maxに戻す
        gaugeImage_.fillAmount = 1f;

        //停止する
        isRunning_ = false;

        backgroundImage_.gameObject.SetActive(false);
        countText_.gameObject.SetActive(false);

        demoImage_.gameObject.SetActive(true);
    }
}