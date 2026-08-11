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

        string debug = "NULL";
        debug = "[効果一覧]\n";
        foreach (var active in player_.Effect.GetActive) debug += $"{active.data_.name_} : {active.timer_}\n";

        debugUI_.text = debug;
    }
}
