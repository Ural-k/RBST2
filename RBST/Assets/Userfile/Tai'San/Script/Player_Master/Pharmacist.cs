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
    int potion_;
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
        int potion = potion_;
        Vector2 pos = Skill.GetNearEnemy(transform.position, 3);

        await Task.Delay(700);//0.7秒
        
        Skill.TakeDamage(Skill.GetHitEnemy(pos, 3), 30);
        switch (potion)
        {
            case 0://水

                break;
        }
    }








    protected override void Skill2(int s)
    {
        switch (nowCombo_[s])
        {
            case 0:
                //potion_ = GetRandomEnum<Potion>();
                break;
        }
    }
    protected override void Skill3(int s)
    {
        
    }
}
