using UnityEngine;
using UnityEngine.UI;

public class PlayerDebugText : MonoBehaviour
{
    private int showPleyer = 0;
    private Text text_;
    private Player player_;
    
    private void Start() { TryGetComponent(out text_); }
    private void Update()
    {
        player_ = PlayerManager.GetPlayer(showPleyer);
        if (!player_) return;
        text_.text = $"[表示] Player{showPleyer}  [ジョブ] : {player_.GetJobData.GetJobName}  [POS] : {player_.transform.position}  [FACE] : {player_.GetLastFace} \n";
        text_.text += $"[GCD] : {player_.GetGCD:0.0}  ";
        text_.text += $"[CD1] : {player_.GetSkillInstance1.cd_:0.0}  ";
        text_.text += $"[CD2] : {player_.GetSkillInstance2.cd_:0.0}  ";
        text_.text += $"[CD3] : {player_.GetSkillInstance3.cd_:0.0}  ";
    }
}
