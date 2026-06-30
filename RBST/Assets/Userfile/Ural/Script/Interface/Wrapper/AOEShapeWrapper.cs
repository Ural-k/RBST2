using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// AOEの種類を格納するenum
/// 追加・削除したい場合はここを変更
/// </summary>
public enum AOECollect
{
    Box,
    Circle,
    Num,
}

[System.Serializable]
public class AOEShapeWrapper
{

    private AOECollect colect_ = new AOECollect();

    /// <summary>
    /// AOEの中身をセットする
    /// 追加・削除したい場合はここを変更
    /// </summary>
    public IAOEshape AOESet(AOECollect colect)
    {
        IAOEshape temp= null;
        switch ((int)colect)
        {
            case (int)AOECollect.Circle:

                temp = new CircleShape();

                return (temp);
            case (int)AOECollect.Box:

                temp = new BoxShape() ;

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

/// <summary>
/// ここから移動用
/// </summary>
[System.Serializable]
public class EnemyMoveWrapper
{
    /// <summary>
    /// 移動処理の識別
    /// </summary>
    public IEnemyMove MoveSet(EnemyMoveCollect moveCollect)
    {
        switch (moveCollect)
        {
            case EnemyMoveCollect.Normal:
                return new NormalMove();

            case EnemyMoveCollect.teleport:
                return new TeleportMove();

            case EnemyMoveCollect.wait:
                //return new WaitMove();

            default:
                return null;
        }
    }
}
