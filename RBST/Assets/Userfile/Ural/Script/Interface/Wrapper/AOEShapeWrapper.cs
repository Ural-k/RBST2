using UnityEngine;

/// <summary>
/// AOEの種類を格納するenum
/// </summary>
public enum AOEColect
{
    Circle,
    Box,
    Donut,
    Num,
}

[System.Serializable]
public class AOEShapeWrapper
{
    private IAOEshape circleShape_;
    private IAOEshape boxShape_;
    private IAOEshape donutShape_;



    private AOEColect colect_ = new AOEColect();

    /// <summary>
    /// AOEの中身をセットする
    /// </summary>
    public void AOESet()
    {
        circleShape_ = new CircleShape();
        boxShape_ = new BoxShape();
        donutShape_ = new DounutShape();
    }


    /// <summary>
    /// AOEがの中身を呼び出す
    /// </summary>
    /// <param name="colect"></param>
    /// <returns></returns>
    public IAOEshape CallAOE(AOEColect colect)
    {
        IAOEshape temp;
        switch ((int)colect)
        {
            case (int)AOEColect.Circle:
                temp = circleShape_;
                return (temp);
            case (int)AOEColect.Box:
                temp = boxShape_;
                return (temp);
            case (int)AOEColect.Donut:
                temp = donutShape_;
                return (temp);
            default:
                return null;
        }
    }

    public float NormalizeInnerFloat(float materialInner,float objectRadius)
    {
        float radius;



        return radius;
    }
}
