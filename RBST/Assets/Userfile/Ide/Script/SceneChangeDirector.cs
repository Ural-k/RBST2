using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeDirector : MonoBehaviour
{
    [SerializeField] private float fadeSpeed_;    //フェード速度(値が大きいほど速くフェードする)
    [SerializeField] public Image fadeImage_;     //フェード用のImage
    private float alpha_ = 0;                     //現在の透明度(0 = 透明、1 = 不透明)

    void Start()
    {
        //初期状態では透明にする
        fadeImage_.color = new Color(0, 0, 0, 0);

        //シーン切り替え処理を開始
        StartCoroutine(SceneChangeCoroutine());
    }

    //シーン切り替え処理
    private IEnumerator SceneChangeCoroutine()
    {
        //Alpha値を1にして画面を黒くする(フェードアウト)
        yield return StartCoroutine(FadeEffect(1));

        //"AfterScene"へ非同期でシーン遷移
        SceneManager.LoadSceneAsync("AfterScene");
    }

    //フェード演出を行うコルーチン
    private IEnumerator FadeEffect(float targetAlpha)
    {
        //現在のAlpha値が目標値に到達するまで繰り返す
        while (!Mathf.Approximately(alpha_, targetAlpha))
        {
            //alphaをtargetAlphaに向かって徐々に変化させる
            alpha_ = Mathf.MoveTowards(alpha_, targetAlpha, fadeSpeed_);

            //Imageの透明度を更新
            fadeImage_.color = new Color(
                fadeImage_.color.r,
                fadeImage_.color.g,
                fadeImage_.color.b,
                alpha_
            );

            //次のフレームまで待機
            yield return null;
        }
    }
}