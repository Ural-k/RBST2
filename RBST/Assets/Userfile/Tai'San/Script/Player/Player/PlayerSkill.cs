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
        if (GCDChecker() || skillIns.cd_ != 0) return skillIns;                 //GCDチェック
        SkillData skillData = skillDataArray[skillIns.nowCombo_];
        gcd_ = skillData.gcd_;//GCD更新

        List<Transform> targetList = GetTarget(skillData.targetType_);          //攻撃対象

        Vector2 resultPos = GetCenter(skillData, GetNear(targetList));          //ターゲット中心

        GoParticle(skillData, resultPos);//パーティクル

        List<Transform> hitResult = GetHit(skillData, targetList, resultPos);   //ヒット判定

        foreach (Transform tf in hitResult) if (tf.GetComponent<TargetCircle>()) tf.GetComponent<IToEnemyDamageAble>().DamageAble(skillData.power_);//ターゲットサークルヘ送る

#if UNITY_EDITOR
        if (demodebug_) Log(skillData, hitResult);                              //コンソールログ
#endif
        /*
         :  戻り値
         */
        if (skillData.motion_.time_ != 0) StartCoroutine(MotionCoroutine(skillData.motion_, resultPos));//モーション情報

        //スキル情報
        SkillInstance resultSkill = new SkillInstance();
        resultSkill.cd_ = skillData.cd_;
        resultSkill.nowCombo_ = skillIns.nowCombo_ + 1 >= skillDataArray.Count() ? 0 : skillIns.nowCombo_ + 1;
        return resultSkill; //skillData.combo_ == SkillName.Null ? skillIns : resultSkill;
    }

    /*
     :  関数---------------------------------------------------------------------------------------------------------------
     */

    private bool GCDChecker()
    {
        return !(gcd_ == 0);
    }

    private List<Transform> GetTarget(TargetType type)
    {
        List<Transform> list = new List<Transform>();
        switch (type)
        {
            case TargetType.Enemy:
                for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).transform);
                break;
            case TargetType.Player:
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) list.Add(PlayerManager.GetPlayer(i).transform);
                break;
            case TargetType.Natural:
                for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).transform);
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) list.Add(PlayerManager.GetPlayer(i).transform);
                break;
        }
        return list;
    }

    private Transform GetNear(List<Transform> targetList)
    {
        return targetList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();
    }

    private Vector2 GetCenter(SkillData data, Transform target)
    {
        if (data.toTarget_)             return target.position + (Vector3)data.offset_;
        else if (data.baseDirection_)   return (Vector2)target.position + lastFace_ * 2;
        return target.position;
    }

    private void GoParticle(SkillData data, Vector2 pos)
    {
        if (data.particle_ != null)
        {
            var ins = Instantiate(data.particle_, pos, Quaternion.identity);
            ins.transform.localScale = data.radius_ == 0 ? data.aspect_ : Vector3.one * data.radius_;
        }
    }

    private List<Transform> GetHit(SkillData data, List<Transform> targetList, Vector2 pos)
    {
        List<Transform> result = new List<Transform>();
        if (data.radius_ == 0 && data.aspect_ == Vector2.zero && GetNear(targetList) != null) { result.Add(GetNear(targetList)); }
        else if (data.radius_ != 0)
        {
            result = targetList.Where(n => Vector2.Distance(pos, n.position) <= data.radius_ + n.transform.localScale.x / 2).ToList();
            LookAt(pos);
        }
        else if (data.aspect_ != Vector2.zero)
        {
            result = targetList.Where(
                //n =>
                //Mathf.Pow(center.position.x - Mathf.Min(Mathf.Max(center.position.x, n.position.x), center.position.x + info.aspect_.x), 2) +
                //Mathf.Pow(center.position.y - Mathf.Min(Mathf.Max(center.position.y, n.position.y), center.position.y + info.aspect_.y), 2)
                //<= n.transform.localScale.x / 2
                n =>
                n.position.x >= pos.x &&                        //centerより右
                n.position.x <= pos.x + data.aspect_.x &&       //center + infoより左
                n.position.y >= pos.y - data.aspect_.y / 2 &&   //center - info/2 より上
                n.position.y <= pos.y + data.aspect_.y / 2      //center + info/2 より下 →Centerは付け根 ※タゲサ非対応につき仮
            ).ToList();
            LookAt(pos, true);
        }
        return result;
    }

    protected void LookAt(Vector3 pos, bool horizontal = false)
    {
        pos += transform.position;
        if (transform.position != pos)
        {
            lastFace_ = (pos - transform.position).normalized;
            if (horizontal) lastFace_ *= Vector2.right;
        }
    }

    private void Log(SkillData data, List<Transform> result)
    {
        string resultText = $"{gameObject.name}の{data.name_}!! →\n";
        foreach (Transform tf in result)
        {
            resultText += $"{tf.gameObject.name}, ";
        }
        if (result.Count != 0)
        {
            resultText += $"に{data.power_}ダメージ!!";
            Debug.Log(resultText);
        }
    }

    /*
     :  コルーチン-----------------------------------------------------------------------------------------------------------
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
            if (motion.jumpOn_)
            {
                Vector3 pos = Vector3.Lerp(startPos, endPos, motion.jumpOrbit_.Evaluate(timer / motion.time_));
                LookAt(pos);
                transform.position = pos;
            }
            yield return null;
        }
        parameter_.speed_ = 5;
    }
}

interface IToEnemyDamageAble { public void DamageAble(int damage); }