using UnityEngine;
using UnityEngine.UI;

public class PlayerDebugText : MonoBehaviour
{
    private readonly int targetPleyer_ = 0;
    private Text text_;
    private Player player_;
    
    private void Start() { TryGetComponent(out text_); }
    private void Update()
    {
        player_ = PlayerManager.GetPlayer(targetPleyer_);
        if (player_ == null) return;
        text_.text = $"[表示] Player{targetPleyer_}  [ジョブ] : {player_.GetInfo.jobData_.GetJobName}  [POS] : {player_.transform.position}  [FACE] : {player_.GetInfo.lastFace_} [FILIP] : {player_.GetInfo.filip_} [LIFE] : {player_.GetInfo.parameter_.HP} / {player_.GetInfo.parameter_.maxHp_} : ";
        text_.text += player_.GetInfo.downTime_ == 0 ? "LIVE" : $"DEATH ({player_.GetInfo.downTime_:0.00})";
        text_.text += $"\n[GCD] : {player_.GetInfo.gcd_:0.00}\n";
        text_.text += $"<skill1> [COMBO] : {player_.GetInfo.skill1_.nowCombo_} / [CD1] : {player_.GetInfo.skill1_.cd_:0.00}\n";
        text_.text += $"<skill2> [COMBO] : {player_.GetInfo.skill2_.nowCombo_} / [CD2] : {player_.GetInfo.skill2_.cd_:0.00}\n";
        text_.text += $"<skill3> [COMBO] : {player_.GetInfo.skill3_.nowCombo_} / [CD3] : {player_.GetInfo.skill3_.cd_:0.00}\n";
        text_.text += $"[PLAYER LIST({PlayerManager.GetAllPlayerListCount()})] :\n";
        if(PlayerManager.GetAllPlayerListCount() != 0)
        {
            for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i)
                text_.text += $"{PlayerManager.GetPlayer(i).name}\n";
        }
        text_.text += $"\n[ENEMY LIST({EnemyManager.GetAllEnemyListCount()})] :\n";
        if(EnemyManager.GetAllEnemyListCount() != 0)
        {
            foreach (EnemyControl e in EnemyManager.GetAllEnemy())
                text_.text += $"{e.name}\n";
        }
    }
}
