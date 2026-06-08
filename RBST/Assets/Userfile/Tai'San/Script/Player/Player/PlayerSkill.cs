using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// スキル関連
/// </summary>
public class PlayerSkill : PlayerStatus
{
    //メモ:マウスカーソル近くの敵に攻撃の処理もほしい

    /// <summary>
    /// スキル発動
    /// </summary>
    /// <param name="skillIns">発動したいスキル</param>
    /// <param name="target">ターゲットを指定</param>
    /// <returns>次に発動できるスキル(コンボ)</returns>
    protected SkillInstance OnSkill(SkillInstance skillIns, SkillData[] skillDataArray, Transform target = null)
    {
        if (gcd_ != 0 || skillIns.cd_ != 0) return skillIns;              //GCDチェック
        SkillData skillData = skillDataArray[skillIns.nowCombo_];
        gcd_ = skillData.gcd_;                                           //GCD更新

        /*
         :  攻撃対象
         */
        List<Transform> targetList = new List<Transform>();
        switch (skillData.targetType_)
        {
            case TargetType.Enemy:
                for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) targetList.Add(EnemyManager.GetEnemy(i).transform);
                break;
            case TargetType.Player:
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
            case TargetType.Natural:
                for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) targetList.Add(EnemyManager.GetEnemy(i).transform);
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
        }

        /*
         :  ターゲット中心
         */
        Transform firstTarget = null;
        Vector2 resultPos = transform.position;
        if (skillData.toTarget_)
        {
            firstTarget = targetList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();
            resultPos = firstTarget.position + (Vector3)skillData.offset_;
        }
        else if (skillData.baseDirection_)
        {
            resultPos += lastFace_;
            Debug.Log($"player{transform.position} : attack{resultPos}");
        }

        /*
         :  パーティクル
         */
        if (skillData.particle_ != null)
        {
            var ins = Instantiate(skillData.particle_, resultPos, Quaternion.identity);
            ins.transform.localScale = skillData.radius_ == 0 ? skillData.aspect_ : Vector3.one * skillData.radius_;
        }

        /*
         :  ヒット判定
         */
        List<Transform> hitResult = new List<Transform>();
        if (skillData.radius_ == 0 && skillData.aspect_ == Vector2.zero && firstTarget != null) { hitResult.Add(firstTarget); }
        else if (skillData.radius_ != 0)
            hitResult = targetList.Where(n => Vector2.Distance(resultPos, n.position) <= skillData.radius_ + n.transform.localScale.x / 2).ToList();
        else if (skillData.aspect_ != Vector2.zero)
            hitResult = targetList.Where(
            //n =>
            //Mathf.Pow(center.position.x - Mathf.Min(Mathf.Max(center.position.x, n.position.x), center.position.x + info.aspect_.x), 2) +
            //Mathf.Pow(center.position.y - Mathf.Min(Mathf.Max(center.position.y, n.position.y), center.position.y + info.aspect_.y), 2)
            //<= n.transform.localScale.x / 2
            n =>
            n.position.x >= resultPos.x &&                        //centerより右
            n.position.x <= resultPos.x + skillData.aspect_.x &&       //center + infoより左
            n.position.y >= resultPos.y - skillData.aspect_.y / 2 &&   //center - info/2 より上
            n.position.y <= resultPos.y + skillData.aspect_.y / 2      //center + info/2 より下 →Centerは付け根 ※タゲサ非対応につき仮
            ).ToList();

        /*
         :  ターゲットサークルヘ送る
         */
        foreach (Transform tr in hitResult)
            if (tr.GetComponent<TargetCircle>()) tr.GetComponent<IToEnemyDamageAble>().DamageAble(skillData.power_);

#if UNITY_EDITOR
        /*
         :  コンソールログ
         */
        if (demodebug_)//仮
        {
            string resultText = $"{gameObject.name}の{skillData.name_}!! →\n";
            foreach (Transform tf in hitResult)
            {
                resultText += $"{tf.gameObject.name}, ";
            }
            if (hitResult.Count != 0)
            {
                resultText += $"に{skillData.power_}ダメージ!!";
                Debug.Log(resultText);
            }
        }
#endif
        /*
         :  戻り値
         */
        //モーション情報
        if (skillData.motion_.time_ != 0) StartCoroutine(MotionCoroutine(skillData.motion_, resultPos));

        //スキル情報
        SkillInstance resultSkill = new SkillInstance();
        resultSkill.cd_ = skillData.cd_;
        resultSkill.nowCombo_ = skillIns.nowCombo_ + 1 >= skillDataArray.Count() ? 0 : skillIns.nowCombo_ + 1;
        return resultSkill; //skillData.combo_ == SkillName.Null ? skillIns : resultSkill;
    }

    /*
     :  コルーチン
     */
    /// <summary>
    /// クールタイム
    /// </summary>
    protected IEnumerator CoolTimeCoroutine()
    {
        while (true)
        {
            gcd_ = Mathf.Max(gcd_ - Time.deltaTime, 0);
            skill1_.cd_ = Mathf.Max(skill1_.cd_ - Time.deltaTime, 0);
            skill2_.cd_ = Mathf.Max(skill2_.cd_ - Time.deltaTime, 0);
            skill3_.cd_ = Mathf.Max(skill3_.cd_ - Time.deltaTime, 0);
            yield return null;
        }
    }

    /// <summary>
    /// スキル使用後のモーション処理
    /// </summary>
    /// <param name="time">時間</param>
    /// <param name="endPos">突進の目標座標</param>
    /// <param name="speed">モーション中の移動速度</param>
    /// <returns></returns>
    protected IEnumerator MotionCoroutine(MotionInfo motion, Vector3 endPos)
    {
        parameter_.speed_ = motion.speed_;
        Vector3 startPos = transform.position;
        float timer = 0;

        while (timer != motion.time_)
        {
            timer = Mathf.Min(timer + Time.deltaTime, motion.time_);
            if (motion.jumpOn_) transform.position = Vector3.Lerp(startPos, endPos, motion.jumpOrbit_.Evaluate(timer / motion.time_));
            yield return null;
        }
        parameter_.speed_ = 5;
    }

}

interface IToEnemyDamageAble { public void DamageAble(int damage); }