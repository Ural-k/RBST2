using UnityEngine;
using UnityEngine.UI;

public class TestPlayerSceneManager : MonoBehaviour
{
    [SerializeField] Text debugtext_;

    private void Awake()
    {
        for (int i = 0; i < 4; ++i) EnemyManager.AddEnemy(GameObject.Find($"Enemy{i + 1}").GetComponent<EnemyControl>());
    }
    private void Update()
    {
        if (!PlayerManager.GetPlayer(0)) return;
        debugtext_.text = $"ƒWƒ‡ƒu : {PlayerManager.GetPlayer(0).GetJobData.GetJobName}\n";
        debugtext_.text += $"GCD : {PlayerManager.GetPlayer(0).GetGCD:0.0}\n";
        debugtext_.text += $"CD1 : {PlayerManager.GetPlayer(0).GetSkillInstance1.cd_:0.0}\n";
        debugtext_.text += $"CD2 : {PlayerManager.GetPlayer(0).GetSkillInstance2.cd_:0.0}\n";
        debugtext_.text += $"CD3 : {PlayerManager.GetPlayer(0).GetSkillInstance3.cd_:0.0}\n";
    }
}
