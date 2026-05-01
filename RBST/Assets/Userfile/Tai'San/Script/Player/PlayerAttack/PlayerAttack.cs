using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttack", menuName = "ScriptableObjects/PlayerAttack")]
public class PlayerAttack : ScriptableObject
{
    [SerializeField] GameObject circleAOE_;
    [SerializeField] GameObject squareAOE_;
    [SerializeField] GameObject triangleAOE_;

    public enum PlayerAttackSphere
    {
        Single,                 //単体攻撃
        Circle,                 //円形
        Square,                 //四角形
        Triangle,               //三角形
        CircularSector,         //扇形
        Donut,                  //ドーナツ形
    }

    public enum PlayerAttackType
    {
        None,       //特に指定無し
        JumpOn,     //飛びつき
    }

    public enum AttackTarget
    {
        None,
        Enemy,  //敵に対して攻撃
        Player, //味方に対して攻撃
        Groval, //無差別攻撃
    }

    public struct PlayerAttackArgument
    {
        public Transform target_;
        public Vector2 position_;
        public Vector2 scale_;
        public Vector2 offset_;
        public PlayerAttackSphere attackSphere_;
        public PlayerAttackType attackType_;
    }

    public struct ModifyStatus
    {
        AttackTarget attackTarget_;
        public bool buff_;
        public float delay_;
        public float lifeTime_;
        //攻撃発動
        //バフアイコン
    }

    /// <summary>
    /// プレイヤーが攻撃する
    /// </summary>
    /// <param name="attack">攻撃の詳細</param>
    /// <param name="status">攻撃時のバフ・デバフ効果の詳細</param>
    public void OnAttack(PlayerAttackArgument attack, ModifyStatus status)
    {
        Transform transform = null;
        if (attack.target_ == null) transform.position = attack.position_;
        else transform = attack.target_;
        //position += attack.offset_;

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