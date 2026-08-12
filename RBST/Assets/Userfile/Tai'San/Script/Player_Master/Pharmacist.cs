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
        //Vector2 pos = Skill.GetNearEnemy(transform.position, Distance(3));

        //await Task.Delay(500);//0.5秒

        //Skill.TakeDamage(Skill.GetHitEnemy(pos, 3), 30);
    }

    /*
     * 1.攻撃系のポーションをランダムに調合する。このスキルは他のコンボを中断しない
     * 2.「残量」をすべて消費して大瓶を投擲する。ポーションの種類によって威力と効果が変わる
     * 
     */
    protected override async void Skill2(int s)
    {
        if (IsGCDCD(s)) return;
        gcd_ = 3.0f;
        switch (nowCombo_[s])
        {
            case 0:
                if (potion_ == Potion.Water)
                {
                    potion_ = (Potion)Random.Range((int)Potion.Water + 1, (int)Potion.Count - 1);
                    ++nowCombo_[s];
                }
                else
                {
                    //別のスキルに変換
                }
                break;
            case 1:
                switch (potion_)
                {
                    case Potion.Attack:
                        var pos = Skill.GetNearEnemy(transform.position, 3);
                        await Task.Delay(500);
                        Skill.TakeDamage(Skill.GetHitEnemy(pos, 3), 1000);
                        break;
                    case Potion.Heal:
                        break;
                    case Potion.Nostrum:
                        break;
                }
                break;
        }
    }

    protected override async void Skill3(int s)
    {
        
    }
}
