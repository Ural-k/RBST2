using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeSpeed_;

    [SerializeField] public Image fadeImage_;

    private float alpha_ = 1;

    void Start()
    {
        //初期状態では不透明にする
        fadeImage_.color = new Color(0, 0, 0, 1);

        StartCoroutine(SceneChangeCoroutine());
    }

    private IEnumerator SceneChangeCoroutine()
    {
        //Alpha値を0にして画面を透明にする(フェードイン)
        yield return StartCoroutine(FadeEffect(0));
    }

    private IEnumerator FadeEffect(float targetAlpha)
    {
        while (!Mathf.Approximately(alpha_, targetAlpha))
        {
            alpha_ = Mathf.MoveTowards(alpha_, targetAlpha, fadeSpeed_);

            fadeImage_.color = new Color(
                fadeImage_.color.r,
                fadeImage_.color.g,
                fadeImage_.color.b,
                alpha_
            );

            yield return null;
        }
    }
}