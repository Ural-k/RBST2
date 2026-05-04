using UnityEngine;
using UnityEngine.UI;

public class DoorDirector : MonoBehaviour
{
    [SerializeField] private Image DoorImage_;      //ドアUI
    [SerializeField] private float minX_;           //ブロックが認識されるXの最小値
    [SerializeField] private float maxX_;           //ブロックが認識されるXの最大値
    [SerializeField] private float minY_;           //ブロックが認識されるYの最小値
    [SerializeField] private float maxY_;           //ブロックが認識されるYの最大値
    public Transform cube_;                         //動かすブロックなど
    public float speed_;                            //ブロックの移動スピード
    private Rigidbody2D playerRigidbody_;           //ブロックのRigidbody2D

    private void Start()
    {
        //ブロックのRigidbody2D
        playerRigidbody_ = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //cube_が既にDestroyされている場合は、それ以降の処理を行わない
        if (cube_ == null)
        {
            return;
        }

        //移動のコード類
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        //今フレームで範囲内か
        bool isInside
            = cube_.position.x
            >= minX_ && cube_.position.x
            <= maxX_ && cube_.position.y
            >= minY_ && cube_.position.y
            <= maxY_;

        //範囲内に入った場合の処理
        if (isInside)
        {
            //cube_のGameObjectを削除
            Destroy(cube_.gameObject);

            //参照をnullにして、以降のUpdateで触らないようにする
            cube_ = null;

            //このフレームの残り処理をスキップ
            return;
        }

        //移動のコード類
        cube_.Translate(move * speed_ * Time.deltaTime);
    }
}