using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// プレイヤーのスキル　攻撃は基本的に円形を指す
/// </summary>
public class Skill
{
    /* ↓↓↓範囲取得↓↓↓ */

    /// <summary>
    /// 近くの敵の座標を取得する
    /// </summary>
    /// <param name="from">元となる座標</param>
    /// <param name="distance">距離</param>
    /// <returns>敵がいない場合xに第２引数を足した値を返す</returns>
    public static Vector2 GetNearEnemy(Vector2 from, float distance = 0)
    {
        if (EnemyManager.GetAllEnemyListCount() == 0) return new Vector2(from.x + distance, from.y);
        else return EnemyManager.GetAllEnemy().OrderBy(n => Vector2.Distance(from, n.transform.position)).FirstOrDefault().gameObject.transform.position;
    }

    /// <summary>
    /// 取得した対象にダメージを与える
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="damage">ダメージ</param>
    public static void TakeDamage(ITestTargetCircle[] target, int damage)
    {
        if(target != null) foreach (var t in target) { t.TakeDamage(damage); }
    }

    /// <summary>
    /// 指定した範囲内の敵を取得する
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public static ITestTargetCircle[] GetHitEnemy(Vector2 center, Vector2 radius)
    {
        if (EnemyManager.GetAllEnemyListCount() != 0)
        {
            List<ITestTargetCircle> target = new List<ITestTargetCircle>();
            for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) target.Add(EnemyManager.GetEnemy(i).GetComponent<ITestTargetCircle>());
            return target.Where(n => 
                Mathf.Pow((n.GetPosition.x - center.x) / (radius.x + n.Radius), 2) +
                Mathf.Pow((n.GetPosition.y - center.y) / (radius.y + n.Radius), 2) <= 1f
            ).ToArray();
        }
        else
        {
            ParticleManager.Instance.SpawnParticleCircle(center, radius, 500, 1);
            return null;
        }
    }
    /// <summary>
    /// 指定した範囲内の敵を取得する
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public static ITestTargetCircle[] GetHitEnemy(Vector2 center, float radius) => GetHitEnemy(center, new Vector2(radius, radius));

    /// <summary>
    /// 指定した範囲内の味方を取得する
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public static ITestTargetCircle[] GetHitPlayer(Vector2 center, Vector2 radius)
    {
        if (PlayerManager.GetAllPlayerListCount() != 0)
        {
            List<ITestTargetCircle> target = new List<ITestTargetCircle>();
            for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) target.Add(PlayerManager.GetPlayer(i).GetComponent<ITestTargetCircle>());
            ParticleManager.Instance.SpawnParticleCircle(center, radius, 500, 1);
            return target.Where(n =>
                Mathf.Pow((n.GetPosition.x - center.x) / (radius.x + n.Radius), 2) +
                Mathf.Pow((n.GetPosition.y - center.y) / (radius.y + n.Radius), 2) <= 1f
            ).ToArray();
        }
        else return null;
    }
}