using UnityEngine;
using UnityEngine.UI;

public class SkillDirector : MonoBehaviour
{
    [SerializeField] private Image skillNameImage_;      //スキルの名前と枠
    [SerializeField] private Image skillImage_;          //スキルのUIの枠
    [SerializeField] private Image demoImage1_;          //仮のUI1
    [SerializeField] private Image demoImage2_;          //仮のUI2
    [SerializeField] private Image demoImage3_;          //仮のUI3
    [SerializeField] private Image demoImage4_;          //仮のUI4
    [SerializeField] private Text skillEffectName1_;     //スキルの効果の名前1
    [SerializeField] private Text skillEffectName2_;     //スキルの効果の名前2
    [SerializeField] private Text skillEffectName3_;     //スキルの効果の名前3
    [SerializeField] private Text skillEffectName4_;     //スキルの効果の名前4

    //オブジェクトが表示されているかどうかを管理するフラグ
    private bool isVisible_ = true;

    private void Start()
    {
        //最初は全部非表示にする
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
        //特定のキー入力で表示させる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //全てのUIの表示・非表示を切り替える
            skillNameImage_.gameObject.SetActive(isVisible_);
            skillImage_.gameObject.SetActive(isVisible_);
            demoImage1_.gameObject.SetActive(isVisible_);
            demoImage2_.gameObject.SetActive(isVisible_);
            demoImage3_.gameObject.SetActive(isVisible_);
            demoImage4_.gameObject.SetActive(isVisible_);
            skillEffectName1_.gameObject.SetActive(isVisible_);
            skillEffectName2_.gameObject.SetActive(isVisible_);
            skillEffectName3_.gameObject.SetActive(isVisible_);
            skillEffectName4_.gameObject.SetActive(isVisible_);

            //フラグを反転
            isVisible_ = !isVisible_;
        }
    }
}