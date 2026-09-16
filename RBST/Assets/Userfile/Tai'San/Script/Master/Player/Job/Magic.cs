using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Magic : Player
{
    private Image hud_;
    private int magicStack_ = 0;

    protected override async void Skill1(int s = 0)
    {
        if (IsGCDCD(s)) return;
        gcd_ = 1.0f;
        var pos0 = Attack.GetNearEnemyPos(transform.position, 4);
        await Task.Delay(500);
        Attack.TakeDamage(Attack.GetHitCircle(Attack.GetAllEnemy(), pos0, 1, Color.white), 120);
        magicStack_ = 0;
    }

    protected override async void Skill2(int s = 1)
    {
        if (IsGCDCD(s)) return;
        if(magicStack_ >= 1 && magicStack_ < 6)//サンダー3回 + 3連魔
        {
            gcd_ = 1.0f;
            var pos0 = Attack.GetNearEnemyPos(transform.position, 4);
            for (int i = 0; i < 3; ++i)
            {
                await Task.Delay(200);
                Attack.TakeDamage(Attack.GetHitCircle(Attack.GetAllEnemy(), pos0, 1, Color.white), 50);
            }
        }
        else if(magicStack_ >= 6 && magicStack_ < 20)
        {
            gcd_ = 1.5f;
            var pos1 = Attack.GetNearEnemyPos(transform.position, 4);
            await Task.Delay(1100);
            Attack.TakeDamage(Attack.GetHitCircle(Attack.GetAllEnemy(), pos1, 1, Color.white), 370);
        }
    }

    protected override void Skill3(int s = 2)
    {

    }
}
