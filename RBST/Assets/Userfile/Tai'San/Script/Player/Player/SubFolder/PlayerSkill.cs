using System.Collections;
using UnityEngine;

/// <summary>
/// スキル関連
/// </summary>
public class PlayerSkill : PlayerVariable
{
    //メモ:マウスカーソル近くの敵に攻撃の処理もほしい

    protected void OnInputSkill(out InputSkillInfo info, int num)
    {
        InputSkillInfo ins = num switch
        {
            INPUT_SKILL_ONE     => info_.skill1_,
            INPUT_SKILL_TWO     => info_.skill2_,
            INPUT_SKILL_THREE   => info_.skill3_,
            _                   => new InputSkillInfo()
        };
        SkillData[] data = num switch
        {
            INPUT_SKILL_ONE     => info_.skillData_.GetSkill1(),
            INPUT_SKILL_TWO     => info_.skillData_.GetSkill2(),
            INPUT_SKILL_THREE   => info_.skillData_.GetSkill3(),
            _                   => new SkillData[0]
        };
        info = SkillFunction.Instance.OnSkill(info_, ins, data);
    }

    /*
     :  コルーチン-----------------------------------------------------------------------------------------------------------
     */

    /// <summary>
    /// クールタイム
    /// </summary>
    protected IEnumerator CoolTimeCoroutine(PlayerInfo player)
    {
        while (true)
        {
            player.gcd_ = Mathf.Max(player.gcd_ - Time.deltaTime, 0);
            player.skill1_.cd_ = Mathf.Max(player.skill1_.cd_ - Time.deltaTime, 0);
            player.skill2_.cd_ = Mathf.Max(player.skill2_.cd_ - Time.deltaTime, 0);
            player.skill3_.cd_ = Mathf.Max(player.skill3_.cd_ - Time.deltaTime, 0);
            yield return null;
        }
    }

    /// <summary>
    ///// スキル使用後のモーション処理
    ///// </summary>
    ///// <param name="motion">モーション情報</param>
    ///// <param name="endPos">突進の目標座標</param>
    ///// <returns></returns>
    //protected IEnumerator MotionCoroutine(MotionInfo motion, Vector3 endPos)
    //{
    //    info_.parameter_.speed_ = motion.speed_;
    //    Vector3 startPos = transform.position;
    //    float timer = 0;

    //    while (timer != motion.time_)
    //    {
    //        timer = Mathf.Min(timer + Time.deltaTime, motion.time_);
    //        if (motion.jumpOn_)
    //        {
    //            Vector3 pos = Vector3.Lerp(startPos, endPos, motion.jumpOrbit_.Evaluate(timer / motion.time_));
    //            info_.LookAt(pos, transform);
    //            transform.position = pos;
    //        }
    //        yield return null;
    //    }
    //    info_.parameter_.speed_ = 5;
    //}
}

interface IToEnemyDamageAble { public void DamageAble(int damage); }