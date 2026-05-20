using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartDirector : MonoBehaviour
{
    [SerializeField] private Image tatebou1_;       //1つ目の縦棒の枠UI
    [SerializeField] private Image tatebou2_;       //2つ目の縦棒の枠UI
    [SerializeField] private Image tatebou3_;       //3つ目の縦棒の枠UI
    [SerializeField] private Image nanamebou1_;     //1つ目の斜め棒の枠UI
    [SerializeField] private Image tatebou4_;       //1つ目の縦棒の塗りUI
    [SerializeField] private Image tatebou5_;       //2つ目の縦棒の塗りUI
    [SerializeField] private Image tatebou6_;       //3つ目の縦棒の塗りUI
    [SerializeField] private Image nanamebou2_;     //1つ目の斜め棒の塗りUI
    public float fillSpeed_;                        //塗るスピード
    public Transform cube_;                         //動かすブロックなど
    public float speed_;                            //ブロックの移動スピード
    [SerializeField] private float minX_;           //ブロックが認識されるXの最小値
    [SerializeField] private float maxX_;           //ブロックが認識されるXの最大値
    [SerializeField] private float minY_;           //ブロックが認識されるYの最小値
    [SerializeField] private float maxY_;           //ブロックが認識されるYの最大値
    private bool wasInside_ = false;                //前フレームでエリア内にいたか
    private Coroutine fillCoroutine_;               //実行中のコルーチン保持
    private bool isFilling_ = false;                //現在塗り処理中かどうか
    private Rigidbody2D playerRigidbody_;           //ブロックのRigidbody2D

    private void Start()
    {
        //枠UIは最初は表示させる
        tatebou1_.gameObject.SetActive(true);
        tatebou2_.gameObject.SetActive(true);
        tatebou3_.gameObject.SetActive(true);
        nanamebou1_.gameObject.SetActive(true);

        //塗りUIは最初は非表示にする
        tatebou4_.gameObject.SetActive(false);
        tatebou5_.gameObject.SetActive(false);
        tatebou6_.gameObject.SetActive(false);
        nanamebou2_.gameObject.SetActive(false);

        //ブロックのRigidbody2D
        playerRigidbody_ = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //これらは念のため置いているので気にしないでください(消しても大丈夫なやつです)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        cube_.Translate(move * speed_ * Time.deltaTime);

        //今フレームで範囲内か
        bool isInside
            = cube_.position.x
            >= minX_ && cube_.position.x 
            <= maxX_ && cube_.position.y 
            >= minY_ && cube_.position.y 
            <= maxY_;

        //範囲外から範囲内に入った時
        if (!wasInside_ && isInside)
        {
            StartFill();
        }

        //範囲内から範囲外に出た時
        if (wasInside_ && !isInside)
        {
            ResetImages();
        }

        //状態更新
        wasInside_ = isInside;
    }

    IEnumerator FillImages()
    {
        //1つ目の縦棒を塗る
        tatebou4_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou4_));

        //2つ目の縦棒を塗る
        tatebou5_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou5_));

        //3つ目の縦棒を塗る
        tatebou6_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou6_));

        //1つ目の斜め棒を塗る
        nanamebou2_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(nanamebou2_));
    }

    //ImageのfillAmountのコルーチン
    IEnumerator FillImage(Image image)
    {
        //初期状態では0にする
        image.fillAmount = 0f;

        //1フレーム待つ
        yield return null;

        float time = 0f;

        //fillAmountが1になるまでループさせる
        while (time < 1f)
        {
            //塗る速度と経過時間を掛けて少しずつ増やす
            time += fillSpeed_ * Time.deltaTime;
            image.fillAmount = time;

            yield return null;
        }

        //最終的に完全塗り
        image.fillAmount = 1f;
    }

    //塗り開始
    void StartFill()
    {
        //実行中なら何もしない
        if(isFilling_)
        {
            return;
        }

        isFilling_ = true;
        fillCoroutine_ = StartCoroutine(FillImages());
    }

    //リセット処理
    void ResetImages()
    {
        //実行中のコルーチン停止
        if (fillCoroutine_ != null)
        {
            StopCoroutine(fillCoroutine_);
            fillCoroutine_ = null;
        }

        isFilling_ = false;

        //塗りUIをリセットする
        ResetImage(tatebou4_);
        ResetImage(tatebou5_);
        ResetImage(tatebou6_);
        ResetImage(nanamebou2_);
    }

    //Imageを初期状態に戻す
    void ResetImage(Image image)
    {
        //塗りを0にして非表示に戻す
        image.fillAmount = 0f;
        image.gameObject.SetActive(false);
    }
}