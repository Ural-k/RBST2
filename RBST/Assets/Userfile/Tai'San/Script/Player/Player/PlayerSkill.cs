using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// スキル関連
/// </summary>
public class PlayerSkill : PlayerStatus
{
    [SerializeField, Header("DEMO")] protected List<Transform> demoEnemyList_;//仮

    //メモ:マウスカーソル近くの敵に攻撃の処理もほしい

    /// <summary>
    /// スキル発動
    /// </summary>
    /// <param name="skill">発動したいスキル</param>
    /// <param name="target">ターゲットを指定</param>
    /// <returns>次に発動できるスキル(コンボ)</returns>
    protected SkillInstance OnSkill(SkillInstance skill, Transform target = null)
    {
        if (gcd_ != 0 || skill.cd_ != 0) return skill;              //GCDチェック
        SkillInfo1 info = SkillData.GetSkillInfo(skill.skillName_); //スキル情報を取得
        gcd_ = info.gcd_;                                           //GCD更新

        /*
         :  攻撃対象
         */
        List<Transform> targetList = new List<Transform>();
        switch (info.targetType_)
        {
            case TargetType.Enemy:
                targetList.AddRange(demoEnemyList_);
                break;
            case TargetType.Player:
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
            case TargetType.Natural:
                targetList.AddRange(demoEnemyList_);
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
        }

        /*
         :  ターゲット中心
         */
        Transform center = transform;
        if (info.toTarget_) center = targetList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();

        /*
         :  パーティクルxo
         */
        if (info.particle_ != null)
        {
            var ins = Instantiate(info.particle_, center.position, Quaternion.identity);
            ins.transform.localScale = info.radius_ == 0 ? info.aspect_ : Vector3.one * info.radius_;
        }

        /*
         :  ヒット判定
         */
        List<Transform> hitResult = new List<Transform>();//GetHit(center, targetList);
        if (info.radius_ == 0 && info.aspect_ == Vector2.zero) { hitResult.Add(center); }
        else if (info.radius_ != 0)
            hitResult = targetList.Where(n => Vector2.Distance(center.position, n.position) <= info.radius_ + n.transform.localScale.x / 2).ToList();
        else if (info.aspect_ != Vector2.zero)
            hitResult = targetList.Where(
            //n =>
            //Mathf.Pow(center.position.x - Mathf.Min(Mathf.Max(center.position.x, n.position.x), center.position.x + info.aspect_.x), 2) +
            //Mathf.Pow(center.position.y - Mathf.Min(Mathf.Max(center.position.y, n.position.y), center.position.y + info.aspect_.y), 2)
            //<= n.transform.localScale.x / 2
            n =>
            n.position.x >= center.position.x &&                        //centerより右
            n.position.x <= center.position.x + info.aspect_.x &&       //center + infoより左
            n.position.y >= center.position.y - info.aspect_.y / 2 &&   //center - info/2 より上
            n.position.y <= center.position.y + info.aspect_.y / 2      //center + info/2 より下 →Centerは付け根 ※タゲサ非対応につき仮
            ).ToList();

        /*
         :  ターゲットサークルヘ送る
         */
        foreach (Transform tr in hitResult)
            if (tr.GetComponent<TargetCircle>()) tr.GetComponent<IToEnemyDamageAble>().DamageAble(info.power_);

#if UNITY_EDITOR
        /*
         :  コンソールログ
         */
        if (log_)//仮
        {
            string resultText = $"{gameObject.name}の{info.name_}!! →\n";
            foreach (Transform tf in hitResult)
            {
                resultText += $"{tf.gameObject.name}, ";
            }
            if (hitResult.Count != 0)
            {
                resultText += $"に{info.power_}ダメージ!!";
                Debug.Log(resultText);
            }
        }
#endif
        /*
         :  戻り値
         */
        //モーション情報
        if (info.motion_.time_ != 0) StartCoroutine(MotionCoroutine(info.motion_, center.position));

        //スキル情報
        SkillInstance resultSkill = new SkillInstance();
        resultSkill.cd_ = info.cd_;
        resultSkill.skillName_ = info.combo_;
        return info.combo_ == SkillName.Null ? skill : resultSkill;
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