using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconHelp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int skillNum_;

    [SerializeField] private GameObject helpImage_;
    [SerializeField] private Text title_;
    [SerializeField] private Text sub_;
    [SerializeField] private Text main_;

    private Player player_;

    private void Start()
    {
        player_ = transform.root.GetComponent<Player>();
    }

    private void Update()
    {
        helpImage_.transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        helpImage_.SetActive(true);

        switch (skillNum_)
        {
            case 1:
                string target = player_.GetSkill1.attacks_[0].GetTargetType switch
                {
                    TargetType.Player => "  =>  味方",
                    TargetType.Enemy => "  =>  敵",
                    _ => " "
                };
                title_.text = player_.GetSkill1.skillName_;
                sub_.text = $"グローバルクールダウン {player_.GetSkill1.gcd_}s  /  クールダウン {player_.GetSkill1.cd_}s\n威力 {player_.GetSkill1.attacks_[0].GetPower}{target}";
                main_.text = player_.GetSkill1.help_;
                break;
            case 2:
                string target1 = player_.GetSkill2.attacks_[0].GetTargetType switch
                {
                    TargetType.Player => "  =>  味方",
                    TargetType.Enemy => "  =>  敵",
                    _ => " "
                };
                title_.text = player_.GetSkill2.skillName_;
                sub_.text = $"グローバルクールダウン {player_.GetSkill2.gcd_}s  /  クールダウン {player_.GetSkill2.cd_}s\n威力 {player_.GetSkill2.attacks_[0].GetPower}{target1}";
                main_.text = player_.GetSkill2.help_;
                break;
            case 3:
                string target2 = player_.GetSkill3.attacks_[0].GetTargetType switch
                {
                    TargetType.Player => "  =>  味方",
                    TargetType.Enemy => "  =>  敵",
                    _ => " "
                };
                title_.text = player_.GetSkill3.skillName_;
                sub_.text = $"グローバルクールダウン {player_.GetSkill3.gcd_}s  /  クールダウン {player_.GetSkill3.cd_}s\n威力 {player_.GetSkill3.attacks_[0].GetPower}{target2}";
                main_.text = player_.GetSkill3.help_;
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        helpImage_.SetActive(false);
    }
}
