using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartDirector : MonoBehaviour
{
    [SerializeField] private Image tatebou1_;       //1‚Â–Ú‚Ìc–_‚Ì˜gUI
    [SerializeField] private Image tatebou2_;       //2‚Â–Ú‚Ìc–_‚Ì˜gUI
    [SerializeField] private Image tatebou3_;       //3‚Â–Ú‚Ìc–_‚Ì˜gUI
    [SerializeField] private Image nanamebou1_;     //1‚Â–Ú‚ÌÎ‚ß–_‚Ì˜gUI
    [SerializeField] private Image tatebou4_;       //1‚Â–Ú‚Ìc–_‚Ì“h‚èUI
    [SerializeField] private Image tatebou5_;       //2‚Â–Ú‚Ìc–_‚Ì“h‚èUI
    [SerializeField] private Image tatebou6_;       //3‚Â–Ú‚Ìc–_‚Ì“h‚èUI
    [SerializeField] private Image nanamebou2_;     //1‚Â–Ú‚ÌÎ‚ß–_‚Ì“h‚èUI
    public float fillSpeed_;                        //“h‚éƒXƒs[ƒh
    public Transform cube;
    public float speed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private bool wasInside = false;
    private Coroutine fillCoroutine;
    private bool isFilling = false;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        //˜gUI‚ÍÅ‰‚Í•\¦‚³‚¹‚é
        tatebou1_.gameObject.SetActive(true);
        tatebou2_.gameObject.SetActive(true);
        tatebou3_.gameObject.SetActive(true);
        nanamebou1_.gameObject.SetActive(true);

        //“h‚èUI‚ÍÅ‰‚Í”ñ•\¦‚É‚·‚é
        tatebou4_.gameObject.SetActive(false);
        tatebou5_.gameObject.SetActive(false);
        tatebou6_.gameObject.SetActive(false);
        nanamebou2_.gameObject.SetActive(false);

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, vertical, 0f);
        cube.Translate(move * speed * Time.deltaTime);

        //¡ƒtƒŒ[ƒ€‚Å”ÍˆÍ“à‚©
        bool isInside = cube.position.x >= minX && cube.position.x <= maxX && cube.position.y >= minY && cube.position.y <= maxY;

        if (!wasInside && isInside)
        {
            StartFill();
        }

        if (wasInside && !isInside)
        {
            ResetImages();
        }

        //ó‘ÔXV
        wasInside = isInside;
    }

    IEnumerator FillImages()
    {
        //1‚Â–Ú‚Ìc–_‚ğ“h‚é
        tatebou4_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou4_));

        //2‚Â–Ú‚Ìc–_‚ğ“h‚é
        tatebou5_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou5_));

        //3‚Â–Ú‚Ìc–_‚ğ“h‚é
        tatebou6_.gameObject.SetActive(true);
        yield return StartCoroutine(FillImage(tatebou6_));

        //1‚Â–Ú‚ÌÎ‚ß–_‚ğ“h‚é
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

    //Image‚ÌfillAmount‚ÌƒRƒ‹[ƒ`ƒ“
    IEnumerator FillImage(Image image)
    {
        //‰Šúó‘Ô‚Å‚Í0‚É‚·‚é
        image.fillAmount = 0f;

        //fillAmount‚ª1‚É‚È‚é‚Ü‚Åƒ‹[ƒv‚³‚¹‚é
        while (image.fillAmount < 1f)
        {
            //“h‚é‘¬“x‚ÆŒo‰ßŠÔ‚ğŠ|‚¯‚Ä­‚µ‚¸‚Â‘‚â‚·
            image.fillAmount += fillSpeed_ * Time.deltaTime;

            //1ƒtƒŒ[ƒ€‘Ò‚Â
            yield return null;
        }

        //ÅI“I‚ÉŠ®‘S“h‚è
        image.fillAmount = 1f;
    }
}