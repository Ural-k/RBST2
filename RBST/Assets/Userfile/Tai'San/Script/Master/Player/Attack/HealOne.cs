using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/HealOne")]
public class HealOne : AttackBase
{
    public override async void Execute(ITargetCircle from, Player player)
    {
        await Task.Delay((int)(delay_ * 1000));
        from.TakeHeal(power_);
    }
}
