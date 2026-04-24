using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartDirector : MonoBehaviour
{
    [SerializeField] private Image tatebou1_;
    [SerializeField] private Image tatebou2_;
    [SerializeField] private Image tatebou3_;
    [SerializeField] private Image nanamebou1_;
    [SerializeField] private Image tatebou4_;
    [SerializeField] private Image tatebou5_;
    [SerializeField] private Image tatebou6_;
    [SerializeField] private Image nanamebou2_;
    public float fillSpeed; // 塗るスピード
    //public float delayBetween; // 1つ目終了後の待機時間

    private void Start()
    {
        tatebou1_.gameObject.SetActive(true);
        tatebou2_.gameObject.SetActive(true);
        tatebou3_.gameObject.SetActive(true);
        nanamebou1_.gameObject.SetActive(true);
        tatebou4_.gameObject.SetActive(false);
        tatebou5_.gameObject.SetActive(false);
        tatebou6_.gameObject.SetActive(false);
        nanamebou2_.gameObject.SetActive(false);
        StartCoroutine(FillImages());
    }

    IEnumerator FillImages()
    {
        // 1つ目
        tatebou4_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou4_));

        //yield return new WaitForSeconds(delayBetween);

        // 2つ目
        tatebou5_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou5_));

        //yield return new WaitForSeconds(delayBetween);

        // 3つ目
        tatebou6_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou6_));

        //yield return new WaitForSeconds(delayBetween);

        // 4つ目
        nanamebou2_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(nanamebou2_));
    }

    IEnumerator FillImage(Image image)
    {
        image.fillAmount = 0f;
        while (image.fillAmount < 1f)
        {
            image.fillAmount += fillSpeed * Time.deltaTime;
            yield return null;
        }
        image.fillAmount = 1f; // 最終的に完全塗り
    }
}