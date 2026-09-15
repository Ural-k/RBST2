using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤーのスキル　攻撃は基本的に円形を指す
/// </summary>
public static class Attack
{
    //--対象----------------------------------------------------------------

    /// <summary>
    /// 全ての敵のタゲサを取得する
    /// </summary>
    public static ITargetCircle[] GetAllEnemy()
    {
        if (EnemyManager.GetAllEnemyListCount() != 0)
        {
            int i = 0;
            ITargetCircle[] result = new ITargetCircle[EnemyManager.GetAllEnemyListCount()];
            foreach (var e in EnemyManager.GetAllEnemy())
            {
                result[i] = (ITargetCircle)e;
                ++i;
            }
            return result;
        }
        else return null;
    }

    /// <summary>
    /// 全てのプレイヤーのタゲサを取得する
    /// </summary>
    public static ITargetCircle[] GetAllPlayer()
    {
        if (PlayerManager.GetAllPlayerListCount() != 0)
        {
            int i = 0;
            ITargetCircle[] result = new ITargetCircle[PlayerManager.GetAllPlayerListCount()];
            foreach (var p in PlayerManager.GetAllPlayer())
            {
                result[i] = (ITargetCircle)p;
                ++i;
            }
            return result;
        }
        else return null;
    }

    /// <summary>
    /// 近くの敵の座標を取得する
    /// </summary>
    /// <param name="from">元となる座標</param>
    /// <param name="distance">敵がいない場合xに第２引数を足した値を返す</param>
    public static Vector2 GetNearEnemyPos(Vector2 from, float distance = 0)
    {
        if (EnemyManager.GetAllEnemyListCount() == 0) return new Vector2(from.x + distance, from.y);
        else return EnemyManager.GetAllEnemy().OrderBy(n => Vector2.Distance(from, n.transform.position)).FirstOrDefault().gameObject.transform.position;
    }

    /// <summary>
    /// 近くの味方の座標を取得する
    /// </summary>
    /// <param name="from">元となる座標</param>
    /// <param name="distance">敵がいない場合xに第２引数を足した値を返す</param>
    public static Vector2 GetNearPlayerPos(Vector2 from, float distance = 0)
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
    public static ITargetCircle[] GetHitCircle(ITargetCircle[] target, Vector2 center, float radius) => GetHitCircle(target, center, new Vector2(radius, radius));

    /// <summary>
    /// 指定した範囲内の敵を取得する
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="radius">半径</param>
    public static ITargetCircle[] GetHitCircle(ITargetCircle[] target, Vector2 center, Vector2 radius)
    {
        AOEManager.Instance.ShowCircle(center, radius);
        if (target == null) return null;
        return target.Where(n =>
            Mathf.Pow((n.GetPosition.x - center.x) / (radius.x + n.Radius), 2) +
            Mathf.Pow((n.GetPosition.y - center.y) / (radius.y + n.Radius), 2) <= 1f
        ).ToArray();
    }

    /// <summary>
    /// 矩形範囲内のタゲサを取得する
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="rotationDeg"></param>
    public static ITargetCircle[] GetHitRect(ITargetCircle[] target, Vector2 center, Vector2 size, float rotationDeg = 0)
    {
        AOEManager.Instance.ShowRectangle(center, size, rotationDeg);
        if (target == null) return null;
        float rotationRad = -rotationDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rotationRad);
        float sin = Mathf.Sin(rotationRad);
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;

        return target.Where(n =>
        {
            float offsetX = n.GetPosition.x - center.x;
            float offsetY = n.GetPosition.y - center.y;
            float localX = offsetX * cos - offsetY * sin;
            float localY = offsetX * sin + offsetY * cos;
            float clampedX = Mathf.Clamp(localX, -halfWidth, halfWidth);
            float clampedY = Mathf.Clamp(localY, -halfHeight, halfHeight);
            return (localX - clampedX) * (localX - clampedX) + (localY - clampedY) * (localY - clampedY) <= n.Radius * n.Radius;
        }).ToArray();
    }

    /// <summary>
    /// ドーナツ範囲のタゲサを取得する
    /// </summary>
    /// <param name="center"></param>
    /// <param name="innerRadius"></param>
    /// <param name="outerRadius"></param>
    /// <returns></returns>
    public static ITargetCircle[] GetHitDonut(ITargetCircle[] target, Vector2 center, float innerRadius, float outerRadius)
    {
        AOEManager.Instance.ShowDonut(center, innerRadius, outerRadius);
        if (target == null) return null;
        return target.Where(n =>
        {
            float distance = Vector2.Distance(n.GetPosition, center);
            return distance + n.Radius > innerRadius && distance - n.Radius < outerRadius;
        }).ToArray();
    }

    /// <summary>
    /// 扇範囲のタゲサを取得する
    /// </summary>
    /// <param name="target"></param>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="dir"></param>
    /// <param name="angleDeg"></param>
    /// <returns></returns>
    public static ITargetCircle[] GetHitFan(ITargetCircle[] target, Vector2 center, float radius, Vector2 dir, float angleDeg)
    {
        AOEManager.Instance.ShowFan(center, angleDeg, dir, radius);
        if (target == null) return null;

        float baseAngleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float halfAngleDeg = angleDeg * 0.5f;

        return target.Where(n =>
        {
            float offsetX = n.GetPosition.x - center.x;
            float offsetY = n.GetPosition.y - center.y;
            float distance = Mathf.Sqrt(offsetX * offsetX + offsetY * offsetY);
            if (distance - n.Radius > radius) return false;
            if (distance <= n.Radius) return true;

            float pointAngleDeg = Mathf.Atan2(offsetY, offsetX) * Mathf.Rad2Deg;
            if (Mathf.Abs(Mathf.DeltaAngle(baseAngleDeg, pointAngleDeg)) <= halfAngleDeg) return true;

            Vector2 edgeStart = center + new Vector2(Mathf.Cos((baseAngleDeg - halfAngleDeg) * Mathf.Deg2Rad), Mathf.Sin((baseAngleDeg - halfAngleDeg) * Mathf.Deg2Rad)) * radius;
            Vector2 edgeEnd = center + new Vector2(Mathf.Cos((baseAngleDeg + halfAngleDeg) * Mathf.Deg2Rad), Mathf.Sin((baseAngleDeg + halfAngleDeg) * Mathf.Deg2Rad)) * radius;

            float tStart = Mathf.Clamp01(Vector2.Dot(n.GetPosition - center, edgeStart - center) / (edgeStart - center).sqrMagnitude);
            float tEnd = Mathf.Clamp01(Vector2.Dot(n.GetPosition - center, edgeEnd - center) / (edgeEnd - center).sqrMagnitude);
            float distToStart = Vector2.Distance(n.GetPosition, center + (edgeStart - center) * tStart);
            float distToEnd = Vector2.Distance(n.GetPosition, center + (edgeEnd - center) * tEnd);

            return Mathf.Min(distToStart, distToEnd) <= n.Radius;
        }).ToArray();
    }

    //--対象へのアクション-----------------------------------------------------

    /// <summary>
    /// 対象にダメージを与える
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="damage">ダメージ</param>
    public static void TakeDamage(ITargetCircle[] target, int damage)
    {
        if (target != null) foreach (var t in target) { t.TakeDamage(damage); }
    }

    /// <summary>
    /// 対象を回復する
    /// </summary>
    /// <param name="target"></param>
    /// <param name="heal"></param>
    public static void TakeHeal(ITargetCircle[] target, int heal)
    {
        if (target != null) foreach (var t in target) { t.TakeHeal(heal); }
    }

    /// <summary>
    /// 対象にバフ・デバフを与える
    /// </summary>
    /// <param name="target"></param>
    public static void TakeEffect(ITargetCircle[] target)
    {

    }
}
