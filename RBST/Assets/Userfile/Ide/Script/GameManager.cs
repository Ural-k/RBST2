using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject courseSelect;

    public GameObject kyaraSelect;

    public Text courseText;

    public Text kyaraText;

    private void Start()
    {
        courseSelect.SetActive(true);

        kyaraSelect.SetActive(true);

        courseText.gameObject.SetActive(false);

        kyaraText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            courseText.gameObject.SetActive(true);

            kyaraText.gameObject.SetActive(true);
        }
    }
}