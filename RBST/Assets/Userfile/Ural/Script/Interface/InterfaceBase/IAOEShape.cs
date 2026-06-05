using UnityEngine;

public interface IAOEshape
{
    /// <summary>
    /// 内側が円状の空洞がある場合のみ使う
    /// </summary>
    public float InnerRadius { get; set; }

    /// <summary>
    /// 各AOEが自分がどの形か記憶出来るようにしておく
    /// </summary>
    public AOECollect AOECollect { get; }

    /// <summary>
    /// AOEのステータスを渡して当たり判定を生成する
    /// </summary>
    /// <param name="radian">AOEの半径</param>
    /// <param name="center">AOEの中心点</param>
    /// <returns></returns>
    Collider2D[] GetHits(float radian, Vector3 center);

    /// <summary>
    /// デバッグ用の当たり判定可視化
    /// </summary>
#if UNITY_EDITOR
    public void OnDrawGizmos(float radius, Vector3 center);
#endif
}
