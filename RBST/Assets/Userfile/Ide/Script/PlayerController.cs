using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cube_;     //実際に移動させるオブジェクト
    [SerializeField] private float speed_;        //移動速度
    private Rigidbody2D playerRigidbody_;         //プレイヤーのRigidbody2D
    private Vector2 move_;                        //入力から計算した移動方向

    private void Start()
    {
        //Rigidbody2Dを取得する
        playerRigidbody_ = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //左右入力
        float horizontal = Input.GetAxisRaw("Horizontal");

        //上下入力
        float vertical = Input.GetAxisRaw("Vertical");

        //入力値から移動方向を作成し、斜め移動でも速度が一定になるよう正規化する
        move_ = new Vector2(horizontal, vertical).normalized;
    }

    //一定時間ごとに呼ばれる物理更新処理
    private void FixedUpdate()
    {
        playerRigidbody_.MovePosition(playerRigidbody_.position + move_ * speed_ * Time.deltaTime);
    }
}