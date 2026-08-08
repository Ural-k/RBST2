using UnityEngine;

public class DemoPlayerManager : MonoBehaviour
{
    [SerializeField] TestPlayerBase player_;
    [SerializeField] EffectData effectData_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player_.GetComponent<ITestTargetCircle>().AddEffect(effectData_, 20);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            player_.GetComponent<ITestTargetCircle>().RemoveEffect(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            player_.GetComponent<ITestTargetCircle>().RemoveEffect(effectData_);
        }
    }
}
