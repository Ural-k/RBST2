using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

/// <summary>
/// 薬師
/// </summary>
public class Pharmacist : Player
{
    //UIはここ
    int amaunt_;//残量?
    Potion potion_;
    enum Potion
    {
        Water,
        Attack,
        Heal,
        Nostrum,//秘薬
        Count
    }
    /* 0.水
     * [1~9は攻撃系]
     * 1.一発攻撃
     * 2.多段攻撃
     * 3.攻撃アップ
     * 4.毒
     * 5.体力を消費して一発攻撃
     * 6.継続ダメージフィールド
     * 7.その他デバフ
     * 8.
     * 9.
     * 
     * [10~は回復系]
     * 10.回復
     * 11.異常状態回復
     * 12.継続回復
     * 13.防御アップ
     * 14.防御フィールド
     * 15.回復フィールド
     * 16.デバフ反転
     * 17.
     * 18.
     * 
     * 
     */

    protected override async void Skill1(int s)
    {
        if (IsGCDCD(s)) return;
        switch (potion_)
        {
            case Potion.Water:
                gcd_ = 1.0f;
                var pos0 = Attack.GetNearEnemy(transform.position, 4);
                await Task.Delay(500);
                Attack.TakeDamage(Attack.GetHitEnemy(pos0, 1), 1);//DPS 1
                break;

            case Potion.Attack:
                gcd_ = 0.5f;
                var pos1 = Attack.GetNearEnemy(transform.position, 4);
                await Task.Delay(350);
                Attack.TakeDamage(Attack.GetHitEnemy(pos1, 1), 130);//DPS 230
                break;

            case Potion.Heal:
                gcd_ = 0.5f;
                var pos2 = Attack.GetNearPlayer(transform.position);
                await Task.Delay(350);
                Attack.TakeHeal(Attack.GetHitPlayer(pos2, 1), 60);//+60
                break;
        }
    }

    /*
     * 1.攻撃系のポーションをランダムに調合する。このスキルは他のコンボを中断しない
     * 2.「残量」をすべて消費して大瓶を投擲する。ポーションの種類によって威力と効果が変わる
     * 
     */
    protected override async void Skill2(int s)
    {
        if (IsGCDCD(s)) return;
        switch (nowCombo_[s])
        {
            case 0://コンボ０
                gcd_ = 3.0f;
                cd_[s] = 5.0f;
                if (potion_ == Potion.Water)
                {
                    potion_ = (Potion)Random.Range((int)Potion.Water + 1, (int)Potion.Count - 1);
                    ++nowCombo_[s];
                    Debug.Log(potion_);
                }
                break;

            case 1://コンボ１
                gcd_ = 1.0f;
                cd_[s] = 3.0f;
                switch (potion_)
                {
                    case Potion.Attack:
                        var pos1 = Attack.GetNearEnemy(transform.position, 5);
                        await Task.Delay(500);
                        Attack.TakeDamage(Attack.GetHitEnemy(pos1, 4), 1000);
                        break;

                    case Potion.Heal:
                        var pos2 = (Vector2)transform.position;
                        await Task.Delay(500);
                        Attack.TakeHeal(Attack.GetHitPlayer(pos2, 4), 700);
                        break;

                    case Potion.Nostrum:
                        break;

                }
                potion_ = Potion.Water;
                nowCombo_[s] = 0;
                break;
        }
    }

    protected override async void Skill3(int s)
    {
        if (IsGCDCD(s)) return;
        var hit = Attack.GetHitPlayer(transform.position, 4);
        
    }
}
