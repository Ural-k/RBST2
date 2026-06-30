using UnityEngine;
public enum EnemyMoveCollect
{
    Normal,
    teleport,
    wait,
}

[System.Serializable]
public struct EnemyMoveStract 
{
    [Header("移動タイプ")]
    public EnemyMoveCollect moveCollect;

    [Header("移動先の座標")]
    public Vector2 pos;

    [Header("移動にかかる時間")]
    public float moveTime;

    [Header("待機時間")]
    public float waitTime;

    [Header("次の移動にかかる時間")]
    public float nextMoveTime;

    [Header("フェード時間")]
    public float fadeTime;

}
