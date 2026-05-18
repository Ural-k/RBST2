using UnityEngine;

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

    public void AOESet()
    {
        circleShape_ = new CircleShape();
        boxShape_ = new BoxShape();
        donutShape_ = new DounutShape();
    }

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
}
