using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cube_;     //実際に移動させるオブジェクト
    [SerializeField] private float speed_;        //移動速度
    private Rigidbody2D playerRigidbody_;         //プレイヤーのRigidbody2D

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

        //入力値を移動ベクトルに変換
        Vector2 move = new Vector2(horizontal, vertical);

        //フレームレートに依存しない速度で移動
        cube_.Translate(move * speed_ * Time.deltaTime);
    }
}