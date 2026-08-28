using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AttackFanction
{
    /// <summary>
    /// プレイヤーのスキル　攻撃は基本的に円形を指す
    /// </summary>
    public static class Attack
    {
        //--近い対象------------------------------------------------------------

        /// <summary>
        /// 近くの敵の座標を取得する
        /// </summary>
        /// <param name="from">元となる座標</param>
        /// <param name="distance">敵がいない場合xに第２引数を足した値を返す</param>
        public static Vector2 GetNearEnemy(Vector2 from, float distance = 0)
        {
            if (EnemyManager.GetAllEnemyListCount() == 0) return new Vector2(from.x + distance, from.y);
            else return EnemyManager.GetAllEnemy().OrderBy(n => Vector2.Distance(from, n.transform.position)).FirstOrDefault().gameObject.transform.position;
        }

        /// <summary>
        /// 近くの味方の座標を取得する
        /// </summary>
        /// <param name="from">元となる座標</param>
        /// <param name="distance">敵がいない場合xに第２引数を足した値を返す</param>
        public static Vector2 GetNearPlayer(Vector2 from, float distance = 0)
        {
            if (PlayerManager.GetAllPlayerListCount() == 0) return new Vector2(from.x + distance, from.y);
            else return PlayerManager.GetAllPlayer().OrderBy(n => Vector2.Distance(from, n.transform.position)).FirstOrDefault().gameObject.transform.position;
        }

        //--範囲取得------------------------------------------------------------

        /// <summary>
        /// 指定した範囲内の敵を取得する
        /// </summary>
        /// <param name="center">中心</param>
        /// <param name="radius">半径</param>
        public static ITargetCircle[] GetHitEnemy(Vector2 center, float radius) => GetHitEnemy(center, new Vector2(radius, radius));

        /// <summary>
        /// 指定した範囲内の敵を取得する
        /// </summary>
        /// <param name="center">中心</param>
        /// <param name="radius">半径</param>
        public static ITargetCircle[] GetHitPlayer(Vector2 center, float radius) => GetHitPlayer(center, new Vector2(radius, radius));

        /// <summary>
        /// 指定した範囲内の敵を取得する
        /// </summary>
        /// <param name="center">中心</param>
        /// <param name="radius">半径</param>
        public static ITargetCircle[] GetHitEnemy(Vector2 center, Vector2 radius)
        {
            if (EnemyManager.GetAllEnemyListCount() != 0)
            {
                List<ITargetCircle> target = new List<ITargetCircle>();
                foreach (var enemy in EnemyManager.GetAllEnemy()) target.Add((ITargetCircle)enemy);
                AOEManager.Instance.ShowCircle(center ,radius);
                return target.Where(n =>
                    Mathf.Pow((n.GetPosition.x - center.x) / (radius.x + n.Radius), 2) +
                    Mathf.Pow((n.GetPosition.y - center.y) / (radius.y + n.Radius), 2) <= 1f
                ).ToArray();
            }
            else
            {
                AOEManager.Instance.ShowCircle(center, radius);
                return null;
            }
        }

        /// <summary>
        /// 指定した範囲内の味方を取得する
        /// </summary>
        /// <param name="center">中心</param>
        /// <param name="radius">半径</param>
        public static ITargetCircle[] GetHitPlayer(Vector2 center, Vector2 radius)
        {
            if (PlayerManager.GetAllPlayerListCount() != 0)
            {
                List<ITargetCircle> target = new List<ITargetCircle>();
                foreach (var player in PlayerManager.GetAllPlayer()) target.Add(player);
                return target.Where(n =>
                    Mathf.Pow((n.GetPosition.x - center.x) / (radius.x + n.Radius), 2) +
                    Mathf.Pow((n.GetPosition.y - center.y) / (radius.y + n.Radius), 2) <= 1f
                ).ToArray();
            }
            else
            {
                return null;
            }
        }

        //--対象へのアクション-----------------------------------------------------

        /// <summary>
        /// 取得した対象にダメージを与える
        /// </summary>
        /// <param name="target">対象</param>
        /// <param name="damage">ダメージ</param>
        public static void TakeDamage(ITargetCircle[] target, int damage)
        {
            if (target != null) foreach (var t in target) { t.TakeDamage(damage); }
        }

        /// <summary>
        /// 取得した対象を回復する
        /// </summary>
        /// <param name="target"></param>
        /// <param name="heal"></param>
        public static void TakeHeal(ITargetCircle[] target, int heal)
        {
            if (target != null) foreach (var t in target) { t.TakeHeal(heal); }
        }

        public static void TakeEffect(ITargetCircle[] target)
        {

        }



        public static bool ContainsLocal(Vector2 wh, Vector2 rotate) =>
            Mathf.Abs(rotate.x) <= wh.x * 0.5f && Mathf.Abs(rotate.y) <= wh.y * 0.5f;
    }
}