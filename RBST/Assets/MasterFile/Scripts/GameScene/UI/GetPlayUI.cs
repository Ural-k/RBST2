using UnityEngine;
using UnityEngine.UI;

public class GetPlayUI : MonoBehaviour
{
    [SerializeField] private Text primary_;
    [SerializeField] private Text secondary_;
    [SerializeField] private Text special_;
    public void SetText(Player player)
    {
        primary_.text = player.GetInfo.jobData_.GetSkill1()[player.GetInfo.skill1_.nowCombo_].name_;
        secondary_.text = player.GetInfo.jobData_.GetSkill2()[player.GetInfo.skill2_.nowCombo_].name_;
        special_.text = player.GetInfo.jobData_.GetSkill3()[player.GetInfo.skill3_.nowCombo_].name_;
    }
}
