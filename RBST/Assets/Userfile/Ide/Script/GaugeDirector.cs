using UnityEngine;
using UnityEngine.UI;

public class GaugeDirector : MonoBehaviour
{
    [SerializeField] private Image gaugeImage_;          //ゲージUI
    [SerializeField] private float maxTime_;             //最大時間(秒)
    private float currentTime_;                          //残り時間
    private bool isRunning_;                             //動作中のフラグ
    private void Start()
    {
        //ゲージUIを非表示にする
        gaugeImage_.gameObject.SetActive(false);
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
        if (currentTime_ < 0f) { isRunning_ = false; }

        //0～1の範囲
        gaugeImage_.fillAmount = currentTime_ / maxTime_;
    }
    //ゲージを開始する
    public void StartGauge()
    {
        //ゲージ開始時に残り時間を最大値に戻し、カウントダウンを開始する
        currentTime_ = maxTime_;
        isRunning_ = true;

        //ゲージUIを表示にする
        gaugeImage_.gameObject.SetActive(true);
    }
}