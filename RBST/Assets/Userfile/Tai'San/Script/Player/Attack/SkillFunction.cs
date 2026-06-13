using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillFunction : MonoBehaviour
{
    static SkillFunction function_ = new SkillFunction();
    public static SkillFunction Instance { get { return function_; } }

    /// <summary>
    /// スキル発動
    /// </summary>
    /// <param name="inputInfo">発動したいスキル</param>
    /// <param name="target">ターゲットを指定</param>
    /// <returns>CD・コンボ情報</returns>
    public InputSkillInfo OnSkill(PlayerInfo player, InputSkillInfo inputInfo, SkillData[] skillDataArray, Transform target = null)
    {
        SkillData       skillData;
        List<Transform> targetList;
        Vector2         resultPos;
        List<Transform> hitResult;

        /*
         :  GCD・CDチェック
         */
        if (GCDChecker(player) || inputInfo.cd_ != 0) return inputInfo;

        /*
         :  現在のコンボに応じたのスキル情報の取得
         */
        skillData = skillDataArray[inputInfo.nowCombo_];

        /*
         :  GCD更新
         */
        player.gcd_ = skillData.gcd_;//GCD更新

        /*
         :  攻撃対象
         */
        targetList = GetTarget(skillData.targetType_);

        /*
         :  攻撃座標の取得
         */
        resultPos = GetCenter(skillData, GetNear(targetList));

        /*
         :  パーティクルの生成
         */
        GoParticle(skillData, resultPos);

        /*
         :  ヒット判定
         */
        hitResult = GetHit(skillData, targetList, resultPos);

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
        if (skillData.motion_.time_ != 0) StartCoroutine(MotionCoroutine(skillData.motion_, resultPos));

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
        bool GCDChecker(PlayerInfo playerInfo)
        {
            return !(playerInfo.gcd_ == 0);
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
            Debug.Log(list[0].gameObject.transform);
            return list;
        }

        Vector2 GetCenter(SkillData data, Transform target)
        {
            if (data.toTarget_) return target.position + (Vector3)data.offset_;
            else if (data.baseDirection_) return (Vector2)target.position + player.lastFace_ * 2;
            return transform.position + (Vector3)data.offset_;
        }

        Transform GetNear(List<Transform> targetList)
        {
            return targetList.OrderBy(n => Vector2.Distance(transform.position, n.position)).First();
        }

        void GoParticle(SkillData data, Vector2 pos)
        {
            if (data.particle_ != null)
            {
                float sign = pos.x >= transform.position.x ? 1 : -1;
                var ins = Instantiate(data.particle_, pos, Quaternion.identity);
                ins.transform.localScale = data.radius_ == 0 ? data.aspect_ * new Vector2(sign, 1) : Vector3.one * data.radius_;
            }
        }

        List<Transform> GetHit(SkillData data, List<Transform> targetList, Vector2 pos)
        {
            List<Transform> result = new List<Transform>();
            if (data.radius_ == 0 && data.aspect_ == Vector2.zero && GetNear(targetList) != null) { result.Add(GetNear(targetList)); }
            else if (data.radius_ != 0)
            {
                player.LookAt(pos, transform);
                result = targetList.Where(n => Vector2.Distance(pos, n.position) <= data.radius_ + n.transform.localScale.x / 2).ToList();
            }
            else if (data.aspect_ != Vector2.zero)
            {
                Vector2 vec = player.LookAt(pos, transform, true);
                float sign = Mathf.Sign(vec.x);
                result = targetList.Where(
                    //n =>
                    //Mathf.Pow(center.position.x - Mathf.Min(Mathf.Max(center.position.x, n.position.x), center.position.x + info.aspect_.x), 2) +
                    //Mathf.Pow(center.position.y - Mathf.Min(Mathf.Max(center.position.y, n.position.y), center.position.y + info.aspect_.y), 2)
                    //<= n.transform.localScale.x / 2
                    n =>
                    n.position.x >= pos.x &&                        //中心より右
                    n.position.x <= (pos.x + data.aspect_.x) * sign &&       //中心 + 先端より左
                    n.position.y >= pos.y - data.aspect_.y / 2 &&   //中心 - 底辺より上
                    n.position.y <= pos.y + data.aspect_.y / 2      //中心 + 上辺より下 →中心は付け根 ※タゲサ範囲無視につき仮
                ).ToList();
            }
            return result;
        }

        void Log(SkillData data, List<Transform> result)
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

        /// <summary>
        /// スキル使用後のモーション処理
        /// </summary>
        /// <param name="motion">モーション情報</param>
        /// <param name="endPos">突進の目標座標</param>
        /// <returns></returns>
        IEnumerator MotionCoroutine(MotionInfo motion, Vector3 endPos)
        {
            player.parameter_.speed_ = motion.speed_;
            Vector3 startPos = transform.position;
            float timer = 0;

            while (timer != motion.time_)
            {
                timer = Mathf.Min(timer + Time.deltaTime, motion.time_);
                if (motion.jumpOn_)
                {
                    Vector3 pos = Vector3.Lerp(startPos, endPos, motion.jumpOrbit_.Evaluate(timer / motion.time_));
                    player.LookAt(pos, transform);
                    transform.position = pos;
                }
                yield return null;
            }
            player.parameter_.speed_ = 5;
        }


    }
}
