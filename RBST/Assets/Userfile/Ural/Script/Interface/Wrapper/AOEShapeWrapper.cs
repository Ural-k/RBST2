using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// AOEの種類を格納するenum
/// 追加・削除したい場合はここを変更
/// </summary>
public enum AOECollect
{
    Circle,
    Box,
    Donut,
    Num,
}

[System.Serializable]
public static class AOEShapeWrapper
{
    private static IAOEshape circleShape_;
    private static IAOEshape boxShape_;
    private static IAOEshape donutShape_;



    private static AOECollect colect_ = new AOECollect();

    /// <summary>
    /// AOEの中身をセットする
    /// 追加・削除したい場合はここを変更
    /// </summary>
    public static void AOESet()
    {
        circleShape_ = new CircleShape();
        boxShape_ = new BoxShape();
        donutShape_ = new DonutShape();
    }


    /// <summary>
    /// AOEがの中身を呼び出す
    /// </summary>
    /// <param name="colect">種類名</param>
    /// <returns></returns>
    public static IAOEshape CallAOE(AOECollect colect)
    {
        IAOEshape temp;
        switch ((int)colect)
        {
            case (int)AOECollect.Circle:
                temp = circleShape_;
                return (temp);
            case (int)AOECollect.Box:
                temp = boxShape_;
                return (temp);
            case (int)AOECollect.Donut:
                temp = donutShape_;
                return (temp);
            default:
                return null;
        }
    }

    /// <summary>
    /// MaterialのInnerRに変換するための関数
    /// </summary>
    /// <param name="inner">オブジェクトのscaleに対する割合</param>
    /// <returns></returns>
    public static float DenomalizeInnerFloat(float inner)
    {
        float innerR;

        innerR = inner / 2;

        return innerR;
    }
}
