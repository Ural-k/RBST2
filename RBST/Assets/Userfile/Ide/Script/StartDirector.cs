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
    public Transform cube;                          //動かすブロックなど
    public float speed;                             //ブロックのスピード
    [SerializeField] private float minX;            //ブロックが認識されるXの最小値
    [SerializeField] private float maxX;            //ブロックが認識されるXの最大値
    [SerializeField] private float minY;            //ブロックが認識されるYの最小値
    [SerializeField] private float maxY;            //ブロックが認識されるYの最大値
    private bool wasInside = false;                 //
    private Coroutine fillCoroutine;                //
    private bool isFilling = false;                 //
    private new Rigidbody2D rigidbody;              //ブロックのRigidbody2D

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
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //これらは念のため置いているので気にしないでください(消しても大丈夫なやつです)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, vertical, 0f);
        cube.Translate(move * speed * Time.deltaTime);

        //今フレームで範囲内か
        bool isInside = cube.position.x >= minX && cube.position.x <= maxX && cube.position.y >= minY && cube.position.y <= maxY;

        if (!wasInside && isInside)
        {
            StartFill();
        }

        if (wasInside && !isInside)
        {
            ResetImages();
        }

        //状態更新
        wasInside = isInside;
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

    void StartFill()
    {
        if(isFilling)
        {
            return;
        }

        isFilling = true;
        fillCoroutine = StartCoroutine(FillImages());
    }

    void ResetImages()
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
        }

        isFilling = false;

        ResetImage(tatebou4_);
        ResetImage(tatebou5_);
        ResetImage(tatebou6_);
        ResetImage(nanamebou2_);
    }

    void ResetImage(Image image)
    {
        image.fillAmount = 0f;
        image.gameObject.SetActive(false);
    }

    //ImageのfillAmountのコルーチン
    IEnumerator FillImage(Image image)
    {
        //初期状態では0にする
        image.fillAmount = 0f;

        //fillAmountが1になるまでループさせる
        while (image.fillAmount < 1f)
        {
            //塗る速度と経過時間を掛けて少しずつ増やす
            image.fillAmount += fillSpeed_ * Time.deltaTime;

            //1フレーム待つ
            yield return null;
        }

        //最終的に完全塗り
        image.fillAmount = 1f;
    }
}