using UnityEngine;
using UnityEngine.UI;

public class PlayerDebugText : MonoBehaviour
{
    private int targetPleyer_ = 0;
    private Text text_;
    private Player player_;
    
    private void Start() { TryGetComponent(out text_); }
    private void Update()
    {
        player_ = PlayerManager.GetPlayer(targetPleyer_);
        if (!player_) return;
        text_.text = $"[表示] Player{targetPleyer_}  [ジョブ] : {player_.GetInfo.skillData_.GetJobName}  [POS] : {player_.transform.position}  [FACE] : {player_.GetInfo.lastFace_} [FILIP] : {player_.GetInfo.filip_}\n";
        text_.text += $"[GCD] : {player_.GetInfo.gcd_:0.00}  ";
        text_.text += $"[CD1] : {player_.GetInfo.skill1_.cd_:0.00}  ";
        text_.text += $"[CD2] : {player_.GetInfo.skill2_.cd_:0.00}  ";
        text_.text += $"[CD3] : {player_.GetInfo.skill3_.cd_:0.00}  \n";
        text_.text += $"[PLAYER LIST({PlayerManager.GetAllPlayerListCount()})] :\n";
        for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i)
            text_.text += $"{PlayerManager.GetPlayer(i).name}\n";
        text_.text += $"\n[ENEMY LIST({EnemyManager.GetAllEnemyListCount()})] :\n";
        foreach (EnemyControl e in EnemyManager.GetAllEnemy())
            text_.text += $"{e.name}\n";
        text_.text += player_.GetInfo.downTime_ == 0 ? "LIVE" : $"DEATH ({player_.GetInfo.downTime_:0.00})";
    }
}
