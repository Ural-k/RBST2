using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            INPUT_SKILL_ONE     => info_.skillData_.GetSkill1(),
            INPUT_SKILL_TWO     => info_.skillData_.GetSkill2(),
            INPUT_SKILL_THREE   => info_.skillData_.GetSkill3(),
            _                   => new SkillData[0]
        };
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
        GoParticle(skillData, targetPos);

        /*
         :  ターゲットサークルヘ結果を送る
         */
        foreach (Transform tf in hitResult) if (tf.GetComponent<TargetCircle>()) tf.GetComponent<IToEnemyDamageAble>().DamageAble(skillData.power_);

#if UNITY_EDITOR
        /*
         :  コンソールログ
         */
        Log(skillData, hitResult);
#endif
        /*
         :  モーション処理
         */
        if (skillData.motion_.time_ != 0) StartCoroutine(MotionCoroutine(skillData.motion_, targetPos));

        /*
         :  CD・コンボ情報の戻り値
         */
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
                    for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).gameObject.transform);
                    break;
                case TargetType.Player:
                    for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) list.Add(PlayerManager.GetPlayer(i).gameObject.transform);
                    break;
                case TargetType.Natural:
                    for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) list.Add(EnemyManager.GetEnemy(i).gameObject.transform);
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
            info_.LookAt(pos, transform, data.skillType_ == SkillType.Square);
            //if(data.skillType_ ==SkillType.Square)
            //{

            //}

            List<Transform> result = data.skillType_ switch
            {
                SkillType.Single => new List<Transform> { GetNear(targetList) },
                SkillType.Circle => targetList.Where(n => Vector2.Distance(pos, n.position) <= data.radius_ + n.transform.localScale.x / 2).ToList(),
                SkillType.Square => targetList.Where(
                    //n =>
                    //Mathf.Pow(center.position.x - Mathf.Min(Mathf.Max(center.position.x, n.position.x), center.position.x + info.aspect_.x), 2) +
                    //Mathf.Pow(center.position.y - Mathf.Min(Mathf.Max(center.position.y, n.position.y), center.position.y + info.aspect_.y), 2)
                    //<= n.transform.localScale.x / 2
                    n =>
                    n.position.x <= transform.position.x + data.aspect_.x &&//Linqが?に対応していないっぽい
                    n.position.y >= transform.position.y - data.aspect_.y / 2 &&   //中心 - 底辺より上
                    n.position.y <= transform.position.y + data.aspect_.y / 2      //中心 + 上辺より下 →中心は付け根 ※タゲサ範囲無視につき仮
                ).ToList(),
                _ => null
            };
            return result;
        }

        void GoParticle(SkillData data, Vector2 pos)
        {
            if (data.particle_ != null)
            {
                if (data.skillType_ == SkillType.Square) pos = transform.position;
                var ins = Instantiate(data.particle_, pos, Quaternion.identity);
                ins.transform.localScale = data.radius_ == 0 ? data.aspect_ * new Vector2(info_.filip_, 1) : Vector3.one * data.radius_;
            }
        }

        void Log(SkillData data, List<Transform> result)
        {
            string resultText = $"{transform.gameObject.name}の{data.name_}!! →\n";
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
            info_.gcd_ = Mathf.Max(info_.gcd_ - Time.deltaTime, 0);
            info_.skill1_.cd_ = Mathf.Max(info_.skill1_.cd_ - Time.deltaTime, 0);
            info_.skill2_.cd_ = Mathf.Max(info_.skill2_.cd_ - Time.deltaTime, 0);
            info_.skill3_.cd_ = Mathf.Max(info_.skill3_.cd_ - Time.deltaTime, 0);
            info_.downTime_ = Mathf.Max(info_.downTime_ - Time.deltaTime, 0);
            
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