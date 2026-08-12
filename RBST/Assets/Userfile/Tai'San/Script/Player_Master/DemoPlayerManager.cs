using UnityEngine;
using UnityEngine.UI;

public class DemoPlayerManager : MonoBehaviour
{
    [SerializeField] Player player_;
    [SerializeField] EffectData effectData_;
    [SerializeField] Text debugUI_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player_.Effect.AddEffect(effectData_, 20);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            player_.Effect.RemoveEffect(5);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            player_.Effect.RemoveEffect(effectData_);
        }

        string debug = "";
        debug += $"[Parameter]";
        debug += $"\nHP: {player_.Parameter.hp_} / {player_.Parameter.maxHp_}";
        debug += $"\nATK: {player_.Parameter.atk_:000}";
        debug += $"\nDEF: {player_.Parameter.def_:000}";
        debug += $"\nSPD: {player_.Parameter.spd_:000}";
        debug += $"\n[GCD]";
        debug += $"\n{player_.GetGCD:000.0}";
        debug += $"\n[CD]";
        debug += $"\n[1] {player_.GetCD[0]:000.0}";
        debug += $"\n[2] {player_.GetCD[1]:000.0}";
        debug += $"\n[3] {player_.GetCD[2]:000.0}";
        debug += "\n[効果一覧]";
        foreach (var active in player_.Effect.GetActive) debug += $"\n{active.data_.name_} : {active.timer_}";

        debugUI_.text = debug;
    }
}
