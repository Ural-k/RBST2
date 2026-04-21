using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject circle_AOE_;
    [SerializeField] GameObject square_AOE_;
    [SerializeField] GameObject triangle_AOE_;

    /// <summary>
    /// スクリーン基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="position">グローバル座標</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Vector2 position, float scale)
    {
        var instance = Instantiate(circle_AOE_, position, Quaternion.identity);
        instance.transform.localScale = new Vector2(scale, scale);
    }

    /// <summary>
    /// ターゲット基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="target">基準のターゲット</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Transform target, float scale)
    {
        var instance = Instantiate(circle_AOE_, target.transform.position, Quaternion.identity);
        instance.transform.localScale = new Vector2(scale, scale);
    }

    /// <summary>
    /// ターゲット基準のプレイヤー円形攻撃範囲
    /// </summary>
    /// <param name="target">基準のターゲット</param>
    /// <param name="offset">中心点</param>
    /// <param name="scale">範囲の大きさ</param>
    public void CircleAttack(Transform target, Vector2 offset, float scale)
    {
        var instance = Instantiate(circle_AOE_, target.transform.position, Quaternion.identity);
        instance.transform.position += (Vector3)offset;
        instance.transform.localScale = new Vector2(scale, scale);
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