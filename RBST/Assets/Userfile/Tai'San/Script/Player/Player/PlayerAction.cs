using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : PlayerStatus
{
    [Header("攻撃の参照")]
    [SerializeField] AttackParticles particles_;
    [SerializeField] PlayerActionReference reference_;
    [SerializeField] List<Transform> demoEnemyList_;

    protected void OnAction(Action name)
    {
        //ベースのアクション情報を取得
        ActionInfo1 info = reference_.GetActionInfo(name);
        //自身のバフ・デバフを参照してアクション情報を変更
        //info.InfoSet(,true);←これを強化終了時にdynamicをfalseにして呼び出し&強化クラスにActionInfo2のListを作ってdynamicをtrueにして呼び出し

        /*
         :  攻撃対象
         */
        List<Transform> targetList = new List<Transform>();
        switch (info.targetType_)
        {
            case TargetType.Enemy:
                targetList = demoEnemyList_;
                break;
            case TargetType.Player:
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
            case TargetType.Natural:
                targetList = demoEnemyList_;
                for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) targetList.Add(PlayerManager.GetPlayer(i).transform);
                break;
        }

        /*
         :  ターゲット中心
         */
        Transform center = transform;
        if (info.toTarget_) center = targetList.OrderBy(n => Vector2.Distance(transform.position, n.transform.position)).First();

        /*
         :  ヒット判定
         */
        List<Transform> hitResult = GetHit(center, targetList);
        List<Transform> GetHit(Transform center, List<Transform> tlist)
        {
            List<Transform> hit = new List<Transform>();
            if (info.radius_ == 0 && info.aspect_ == Vector2.zero) { hit.Add(center); return hit; }
            else if (info.radius_ != 0)
            {
                var ins = Instantiate(particles_.fire_, center.position, Quaternion.identity);  //パーティクル
                ins.transform.localScale = Vector2.one * info.radius_;
                hit = tlist.Where(n => Vector2.Distance(center.position, n.position) <= info.radius_).ToList();
            }
            else if (info.aspect_ != Vector2.zero)
            {
                var ins = Instantiate(particles_.meteor_, center.position, Quaternion.identity);
                ins.transform.localScale = info.aspect_;
                hit = tlist.Where(
                n =>
                n.position.x >= center.position.x &&                        //centerより右
                n.position.x <= center.position.x + info.aspect_.x &&       //center + infoより左
                n.position.y >= center.position.y - info.aspect_.y / 2 &&   //center - info/2 より上
                n.position.y <= center.position.y + info.aspect_.y / 2      //center + info/2 より下 →Centerは付け根
                ).ToList();
            }
            return hit;
        }

        /*
         :  ターゲットサークルヘ送る
         */
        foreach (Transform tr in hitResult)
            if (tr.GetComponent<TargetCircle>()) tr.GetComponent<TargetCircle>().DamageAble(info.power_);

        /*
         :  ログ
         */
        string resultText = $"{gameObject.name}の{info.name_}!! →\n";
        foreach (Transform tr in hitResult)
        {
            resultText += $"{tr.gameObject.name}, ";
        }
        if(hitResult.Count != 0)
        {
            resultText += $"に{info.power_}ダメージ!!";
            Debug.Log(resultText);
        }
    }
}

interface IToEnemyDamageAble
{
    public void DamageAble(int damage);
}