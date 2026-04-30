using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttack", menuName = "ScriptableObjects/PlayerAttack")]
public class PlayerAttack : ScriptableObject
{
    [SerializeField] GameObject circleAOE_;
    [SerializeField] GameObject squareAOE_;
    [SerializeField] GameObject triangleAOE_;

    enum Shape
    {
        Circle,
        Square,
        Triangle
    }

    /// <summary>
    /// スクリーン基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="position">グローバル座標</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Vector2 position, Vector2 scale)
    {
        var instance = Instantiate(circleAOE_, position, Quaternion.identity);
        instance.transform.localScale = scale;
    }

    /// <summary>
    /// ターゲット基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="target">基準のターゲット</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Transform target, Vector2 scale)
    {
        var instance = Instantiate(circleAOE_, target.transform.position, Quaternion.identity);
        instance.transform.localScale = scale;
    }

    /// <summary>
    /// ターゲット基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="target">基準のターゲット</param>
    /// <param name="offset">中心点</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Transform target, Vector2 offset, Vector2 scale)
    {
        var instance = Instantiate(circleAOE_, target.transform.position, Quaternion.identity);
        instance.transform.position += (Vector3)offset;
        instance.transform.localScale = scale;
    }

    public void SquareAttack(Vector2 position, Vector2 scale)
    {
        var instance = Instantiate(squareAOE_, position, Quaternion.identity);
        instance.transform.localScale = scale;
    }

    public void SquareAttack(Transform target, Vector2 scale)
    {
        var instance = Instantiate(squareAOE_, target.transform.position, Quaternion.identity);
        instance.transform.localScale = scale;
    }


    //↓未実装
#if false
    /// <summary>
    /// プレイヤーの向き基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="distance">プレイヤー正面からの距離</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(float distance,float scale)
    {

    }
    /// <summary>
    /// プレイヤーの向き基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="distance">プレイヤー正面からの距離</param>
    /// <param name="scale">範囲の大きさ</param>
    /// <param name="offset">distanceからの中心点</param>
    public void CircleAttack(float distance, float scale, Vector2 offset)
    {

    }
#endif
}