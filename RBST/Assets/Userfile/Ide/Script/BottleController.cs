using UnityEngine;

public class BottleController : MonoBehaviour
{
<<<<<<< HEAD
    //ボトルが飛ぶ速度
    [SerializeField] private float speed;

    //ボトルの回転速度
    [SerializeField] private float rotateSpeed;

    //ボトルが存在できる時間
    [SerializeField] private float lifeTime;

    //ボトルが進む方向
    //初期値は右方向
    private Vector2 direction = Vector2.right;
=======
    //ボトルが飛ぶ速度(speed×lifeTimeの計算で遠くへ飛ぶ(速度が速いと瓶の飛び方自体も変わる))
    [SerializeField] private float speed_;

    //ボトルの回転速度
    [SerializeField] private float rotateSpeed_;

    //ボトルが存在できる時間
    [SerializeField] private float lifeTime_;

    //ボトルが飛ぶ最大距離
    [SerializeField] private float maxDistance_;

    //ボトルが進む方向
    //初期値は右方向
    private Vector2 direction_ = Vector2.right;

    //投げられた位置を保存
    private Vector2 startPosition_;
>>>>>>> feature/BetaUI

    void Start()
    {
        //lifeTime秒後にボトルを削除する
<<<<<<< HEAD
        Destroy(gameObject, lifeTime);
    }

    //プレイヤー側から投げる方向を受け取る処理
    //PlayerControllerから呼び出される
    public void SetDirection(Vector2 dir)
    {
        //受け取った方向を移動方向として保存
        direction = dir;
=======
        Destroy(gameObject, lifeTime_);

        //投げられた瞬間の位置を保存
        startPosition_ = transform.position;
>>>>>>> feature/BetaUI
    }

    //毎フレーム呼ばれる処理
    void Update()
    {
        //設定された方向へボトルを移動させる
        //Time.deltaTimeを使うことでフレームレートに左右されない速度になる
<<<<<<< HEAD
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        //ボトルを回転させる
        //投げられている感じを出すための演出
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
=======
        transform.position += (Vector3)(direction_ * speed_ * Time.deltaTime);

        //ボトルを回転させる
        //投げられている感じを出すための演出
        transform.Rotate(0, 0, rotateSpeed_ * Time.deltaTime);

        //投げた場所からの距離を確認
        float distance = Vector2.Distance(startPosition_, transform.position);

        //一定距離飛んだら消す
        if (distance >= maxDistance_)
        {
            Destroy(gameObject);
        }
    }

    //プレイヤー側から投げる方向を受け取る処理
    //PlayerControllerから呼び出される
    public void SetDirection(Vector2 direction)
    {
        //受け取った方向を移動方向として保存
        direction_ = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemyのタグが付いた敵に当たったら
        if(collision.CompareTag("EnemyTag"))
        {
            //ボトルを消す
            Destroy(gameObject);
        }
>>>>>>> feature/BetaUI
    }
}