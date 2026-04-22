using UnityEngine;
using UnityEngine.UI;

public class SkillDirector : MonoBehaviour
{
    [SerializeField] private Image skillNameImage_;
    [SerializeField] private Image skillImage_;
    [SerializeField] private Image demoImage1_;
    [SerializeField] private Image demoImage2_;
    [SerializeField] private Image demoImage3_;
    [SerializeField] private Image demoImage4_;
    [SerializeField] private Text skillEffectName1_;
    [SerializeField] private Text skillEffectName2_;
    [SerializeField] private Text skillEffectName3_;
    [SerializeField] private Text skillEffectName4_;

    private void Start()
    {
        skillNameImage_.gameObject.SetActive(false);
        skillImage_.gameObject.SetActive(false);
        demoImage1_.gameObject.SetActive(false);
        demoImage2_.gameObject.SetActive(false);
        demoImage3_.gameObject.SetActive(false);
        demoImage4_.gameObject.SetActive(false);
        skillEffectName1_.gameObject.SetActive(false);
        skillEffectName2_.gameObject.SetActive(false);
        skillEffectName3_.gameObject.SetActive(false);
        skillEffectName4_.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skillNameImage_.gameObject.SetActive(true);
            skillImage_.gameObject.SetActive(true);
            demoImage1_.gameObject.SetActive(true);
            demoImage2_.gameObject.SetActive(true);
            demoImage3_.gameObject.SetActive(true);
            demoImage4_.gameObject.SetActive(true);
            skillEffectName1_.gameObject.SetActive(true);
            skillEffectName2_.gameObject.SetActive(true);
            skillEffectName3_.gameObject.SetActive(true);
            skillEffectName4_.gameObject.SetActive(true);
        }
        else
        {
            skillNameImage_.gameObject.SetActive(false);
            skillImage_.gameObject.SetActive(false);
            demoImage1_.gameObject.SetActive(false);
            demoImage2_.gameObject.SetActive(false);
            demoImage3_.gameObject.SetActive(false);
            demoImage4_.gameObject.SetActive(false);
            skillEffectName1_.gameObject.SetActive(false);
            skillEffectName2_.gameObject.SetActive(false);
            skillEffectName3_.gameObject.SetActive(false);
            skillEffectName4_.gameObject.SetActive(false);
        }
    }
}