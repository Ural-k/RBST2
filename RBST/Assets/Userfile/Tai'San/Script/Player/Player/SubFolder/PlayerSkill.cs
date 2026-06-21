using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// スキル関連
/// </summary>
public class PlayerSkill : PlayerVariable
{
    //メモ:マウスカーソル近くの敵に攻撃の処理もほしい LookAt

    protected void OnInputSkill(out InputSkillInfo info, int input)
    {
        InputSkillInfo insInfo = input switch
        {
            INPUT_SKILL_ONE     => info_.skill1_,
            INPUT_SKILL_TWO     => info_.skill2_,
            INPUT_SKILL_THREE   => info_.skill3_,
            _                   => new InputSkillInfo()
        };
        SkillData[] data = input switch
        {
            INPUT_SKILL_ONE     => info_.jobData_.GetSkill1(),
            INPUT_SKILL_TWO     => info_.jobData_.GetSkill2(),
            INPUT_SKILL_THREE   => info_.jobData_.GetSkill3(),
            _                   => new SkillData[0]
        };
        if (info_.lastInput_ != input)
        {
            info_.skill1_.nowCombo_ = 0;
            info_.skill2_.nowCombo_ = 0;
            info_.skill3_.nowCombo_ = 0;
        }
        info = OnSkill(insInfo, data);
    }

    /// <summary>
    /// スキル発動
    /// </summary>
    /// <param name="inputInfo">発動したいスキル</param>
    /// <param name="target">ターゲットを指定</param>
    /// <returns>CD・コンボ情報</returns>
    public InputSkillInfo OnSkill(InputSkillInfo inputInfo, SkillData[] skillDataArray/*, Transform target = null*/)
    {
        SkillData skillData;
        List<Transform> targetList;
        Vector2 targetPos;
        List<Transform> hitResult;

        /*
         :  GCD・CDチェック
         */
        if (GCDChecker() || inputInfo.cd_ != 0) return inputInfo;

        /*
         :  現在のコンボに応じたのスキル情報の取得
         */
        skillData = skillDataArray[inputInfo.nowCombo_];

        /*
         :  GCD更新
         */
        info_.gcd_ = skillData.gcd_;

        /*
         :  攻撃対象になりえるオブジェクトを取得(ENEMY or PLAYER)
         */
        targetList = GetTarget(skillData.targetType_);

        /*
         :  空振り
         */
        if (targetList.Count == 0)
        {
            ParticleManager.Instance.SpawnParticle(skillData, transform.position, transform.position, info_.filip_);
            //GoParticle(skillData, transform.position);
            return new InputSkillInfo { cd_ = skillData.cd_, nowCombo_ = 0 };
        }

        /*
         :  攻撃座標の取得
         */
        targetPos = GetTargetPos(skillData, targetList);

        /*
         :  ヒット結果(範囲内)
         */
        hitResult = GetHit(skillData, targetList, targetPos);

        /*
         :  パーティクルの生成 (メモ:ここでビーム系攻撃か円形かなどで向きが変わるからenumとかで攻撃形状を把握できるようにする)
         */
        ParticleManager.Instance.SpawnParticle(skillData, targetPos, transform.position, info_.filip_);

        /*
         :  ターゲットサークルヘ結果を送る ※TargetCircle->EnemyControll
         */
        foreach (Transform tf in hitResult) if (tf.GetComponent<TargetCircle>()) tf.GetComponent<IToEnemyDamageAble>().DamageAble(GetDamage(skillData));

        /*
         :  モーション処理
         */
        if (skillData.motion_.time_ != 0) StartCoroutine(MotionCoroutine(skillData.motion_, targetPos));

        /*
         :  CD・コンボ情報の戻り値
         */
        info_.activeCombo_ = ACTIVE_COMBO_SECOND;
        return new InputSkillInfo { cd_ = skillData.cd_, nowCombo_ = inputInfo.nowCombo_ + 1 >= skillDataArray.Count() ? 0 : inputInfo.nowCombo_ + 1 };


        /*
         :  ===========================================================================================================================================
         */

        /// <summary>
        /// GCDチェッカー
        /// </summary>
        /// <param name="playerInfo">プレイヤー情報</param>
        /// <returns>GCDが0以外の時trueを返す</returns>
        bool GCDChecker()
        {
            return !(info_.gcd_ == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Transform> GetTarget(TargetType type)
        {
            List<Transform> list = new List<Transform>();
            switch (type)
            {
                case TargetType.Enemy:
                    if(EnemyManager.GetAllEnemyListCount() != 0)
                        for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).gameObject.transform);
                    break;
                case TargetType.Player:
                    if(PlayerManager.GetAllPlayerListCount() != 0) 
                        for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) list.Add(PlayerManager.GetPlayer(i).gameObject.transform);
                    break;
                case TargetType.Natural:
                    if (EnemyManager.GetAllEnemyListCount() != 0) 
                        for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).gameObject.transform);
                    if (PlayerManager.GetAllPlayerListCount() != 0) 
                        for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) list.Add(PlayerManager.GetPlayer(i).gameObject.transform);
                    break;
            }
            return list;
        }

        Vector2 GetTargetPos(SkillData data, List<Transform> list)
        {
            var nearTarger = GetNear(list);
            if (data.toTarget_) return nearTarger.position + (Vector3)data.offset_;
            else if (data.baseDirection_) return (Vector2)transform.position + info_.lastFace_;
            return transform.position + (Vector3)data.offset_;
        }

        Transform GetNear(List<Transform> targetList)
        {
            return targetList.OrderBy(n => Vector2.Distance(transform.position, n.position)).First();
        }

        List<Transform> GetHit(SkillData data, List<Transform> targetList, Vector2 pos)
        {
            info_.LookAt(pos, transform, data.shape_ == SkillShape.Square);
            List<Transform> result = data.shape_ switch
            {
                SkillShape.Single => new List<Transform> { GetNear(targetList) },//max(xmin, min(cx, xmax)) / max(ymin, min(cy, ymax))
                SkillShape.Circle => targetList.Where(n => Vector2.Distance(pos, n.position) <= data.scale_.x + n.GetComponent<TargetCircle>().GetRadius).ToList(),
                SkillShape.Square => targetList.Where(
                n => Vector2.Distance(
                    new Vector2(
                        Mathf.Clamp(n.position.x, Mathf.Min(transform.position.x, transform.position.x + data.scale_.x * info_.filip_),
                        Mathf.Max(transform.position.x, transform.position.x + data.scale_.x * info_.filip_)),
                        Mathf.Max(transform.position.y - data.scale_.y / 2, Mathf.Min(n.position.y, transform.position.y + data.scale_.y / 2))),
                    n.position) < n.GetComponent<TargetCircle>().GetRadius
                ).ToList(),
                _ => new List<Transform>()
            };
            return result;
        }

        int GetDamage(SkillData data)
        {
            return Mathf.Max(data.power_ + Random.Range(-RANDOME_DAMAGE_RANGE,RANDOME_DAMAGE_RANGE), 1);
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
        Transform lastTarget = null;
        while (true)
        {
            info_.gcd_ = Mathf.Max(info_.gcd_ - Time.deltaTime, 0);
            info_.skill1_.cd_ = Mathf.Max(info_.skill1_.cd_ - Time.deltaTime, 0);
            info_.skill2_.cd_ = Mathf.Max(info_.skill2_.cd_ - Time.deltaTime, 0);
            info_.skill3_.cd_ = Mathf.Max(info_.skill3_.cd_ - Time.deltaTime, 0);
            info_.downTime_ = Mathf.Max(info_.downTime_ - Time.deltaTime, 0);
            info_.effect_.AllEffectTimer();
            yield return null;
        }
    }

    /// <summary>
    /// スキル使用後のモーション処理
    /// </summary>
    /// <param name="motion">モーション情報</param>
    /// <param name="endPos">突進の目標座標</param>
    /// <returns></returns>
    protected IEnumerator MotionCoroutine(MotionInfo motion, Vector3 endPos)
    {
        info_.parameter_.speed_ = motion.speed_;
        Vector3 startPos = transform.position;
        float timer = 0;

        while (timer != motion.time_)
        {
            timer = Mathf.Min(timer + Time.deltaTime, motion.time_);
            if (motion.jumpOn_)
            {
                Vector3 pos = Vector3.Lerp(startPos, endPos, motion.jumpOrbit_.Evaluate(timer / motion.time_));
                info_.LookAt(pos, transform);
                transform.position = pos;
            }
            yield return null;
        }
        info_.parameter_.speed_ = 5;
    }
}

interface IToEnemyDamageAble { public void DamageAble(int damage); }