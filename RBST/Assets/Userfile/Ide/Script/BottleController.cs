using UnityEngine;

public class BottleController : MonoBehaviour
{
    //ボトルが飛ぶ速度
    public float speed;

    //ボトルの回転速度
    public float rotateSpeed;

    //ボトルが存在できる時間
    //時間が経過すると自動的に削除される
    public float lifeTime;

    //ボトルが進む方向
    //初期値は右方向
    private Vector2 direction = Vector2.right;

    void Start()
    {
        //lifeTime秒後にボトルを削除する
        Destroy(gameObject, lifeTime);
    }

    //プレイヤー側から投げる方向を受け取る処理
    //PlayerControllerから呼び出される
    public void SetDirection(Vector2 dir)
    {
        //受け取った方向を移動方向として保存
        direction = dir;
    }

    //毎フレーム呼ばれる処理
    void Update()
    {
        //設定された方向へボトルを移動させる
        //Time.deltaTimeを使うことでフレームレートに左右されない速度になる
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        //ボトルを回転させる
        //投げられている感じを出すための演出
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}