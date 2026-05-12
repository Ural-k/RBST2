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
    public Transform cube2_;
    public float speed2_;
    public Transform cube3_;
    public float speed3_;
    public Transform cube4_;
    public float speed4_;
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
            if(cube2_ == null)
            {
                if(cube3_ == null)
                {
                    if(cube4_ == null)
                    {
                        return;
                    }
                }
            }
        }

        //移動のコード類
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        float horizontal2 = Input.GetAxisRaw("Horizontal2");
        float vertical2 = Input.GetAxisRaw("Vertical2");
        Vector2 move2 = new Vector2(horizontal2, vertical2);

        float horizontal3 = Input.GetAxisRaw("Horizontal3");
        float vertical3 = Input.GetAxisRaw("Vertical3");
        Vector2 move3 = new Vector2(horizontal3, vertical3);

        float horizontal4 = Input.GetAxisRaw("Horizontal4");
        float vertical4 = Input.GetAxisRaw("Vertical4");
        Vector2 move4 = new Vector2(horizontal4, vertical4);

        //今フレームで範囲内か
        bool isInside
            = cube_.position.x
            >= minX_ && cube_.position.x
            <= maxX_ && cube_.position.y
            >= minY_ && cube_.position.y
            <= maxY_;
        bool isInside2
            = cube2_.position.x
            >= minX_ && cube_.position.x
            <= maxX_ && cube_.position.y
            >= minY_ && cube_.position.y
            <= maxY_;
        bool isInside3
            = cube3_.position.x
            >= minX_ && cube_.position.x
            <= maxX_ && cube_.position.y
            >= minY_ && cube_.position.y
            <= maxY_;
        bool isInside4
            = cube4_.position.x
            >= minX_ && cube_.position.x
            <= maxX_ && cube_.position.y
            >= minY_ && cube_.position.y
            <= maxY_;

        //範囲内に入った場合の処理
        if (isInside)
        {
            if(isInside2)
            {
                if(isInside3)
                {
                    if(isInside4)
                    {
                        //cube_のGameObjectを削除
                        Destroy(cube_.gameObject);
                        Destroy(cube2_.gameObject);
                        Destroy(cube3_.gameObject);
                        Destroy(cube4_.gameObject);

                        //参照をnullにして、以降のUpdateで触らないようにする
                        cube_ = null;
                        cube2_ = null;
                        cube3_ = null;
                        cube4_ = null;

                        //このフレームの残り処理をスキップ
                        return;
                    }
                }
            }
        }

        //移動のコード類
        cube_.Translate(move * speed_ * Time.deltaTime);

        cube2_.Translate(move2 * speed2_ * Time.deltaTime);

        cube3_.Translate(move3 * speed3_ * Time.deltaTime);

        cube4_.Translate(move4 * speed4_ * Time.deltaTime);
    }
}