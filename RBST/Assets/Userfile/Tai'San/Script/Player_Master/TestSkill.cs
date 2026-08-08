using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤーのスキル　攻撃は基本的に円形を指す
/// </summary>
public class TestSkill : MonoBehaviour
{
    public static TestSkill Instance { get; }

    /* ↓↓↓範囲取得↓↓↓ */

    /// <summary>
    /// 一番近い敵中心の範囲に当たった敵の取得
    /// </summary>
    /// <param name="from">基準座標</param>
    /// <param name="radius">半径</param>
    public Transform[] GetHitEnemyNear(Vector2 from, float radius)
    {
        if (EnemyManager.GetAllEnemyListCount() != 0)
        {
            List<Transform> target = new List<Transform>();
            for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) target.Add(EnemyManager.GetEnemy(i).gameObject.transform);
            var near = target.OrderBy(n => Vector2.Distance(from, n.position)).First().position;
            ParticleManager.Instance.SpawnParticleCircle(near, new Vector2(radius, radius), 500, 1);
            return target.Where(n => Vector2.Distance(near, n.position) <= radius + n.GetComponent<TargetCircle>().GetRadius).ToArray();
        }
        else return null;
    }
    /// <summary>
    /// 範囲に当たった敵の取得
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public Transform[] GetHitEnemy(Vector2 center, float radius)
    {
        if (EnemyManager.GetAllEnemyListCount() != 0)
        {
            List<Transform> target = new List<Transform>();
            for (int i = 0; i < EnemyManager.GetAllEnemyListCount(); ++i) target.Add(EnemyManager.GetEnemy(i).gameObject.transform);
            ParticleManager.Instance.SpawnParticleCircle(center, new Vector2(radius, radius), 500, 1);
            return target.Where(n => Vector2.Distance(center, n.position) <= radius + n.GetComponent<TargetCircle>().GetRadius).ToArray();
        }
        else return null;
    }
    /// <summary>
    /// 一番近い味方中心の範囲に当たった味方の取得
    /// </summary>
    /// <param name="from">基準座標</param>
    /// <param name="radius">半径</param>
    public Transform[] GetHitPlayerNear(Vector2 from, float radius)
    {
        if (PlayerManager.GetAllPlayerListCount() != 0)
        {
            List<Transform> target = new List<Transform>();
            for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) target.Add(PlayerManager.GetPlayer(i).gameObject.transform);
            var near = target.OrderBy(n => Vector2.Distance(from, n.position)).First().position;
            ParticleManager.Instance.SpawnParticleCircle(near, new Vector2(radius, radius), 500, 1);
            return target.Where(n => Vector2.Distance(near, n.position) <= radius + n.GetComponent<TargetCircle>().GetRadius).ToArray();
        }
        else return null;
    }
    /// <summary>
    /// 範囲に当たった味方の取得
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public Transform[] GetHitPlayer(Vector2 center, float radius)
    {
        if (PlayerManager.GetAllPlayerListCount() != 0)
        {
            List<Transform> target = new List<Transform>();
            for (int i = 0; i < PlayerManager.GetAllPlayerListCount(); ++i) target.Add(PlayerManager.GetPlayer(i).gameObject.transform);
            ParticleManager.Instance.SpawnParticleCircle(center, new Vector2(radius, radius), 500, 1);
            return target.Where(n => Vector2.Distance(center, n.position) <= radius + n.GetComponent<TargetCircle>().GetRadius).ToArray();
        }
        else return null;

    }
}