using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの移動速度
    public float speed;

    //プレイヤーアニメーションを制御するスクリプト
    public PlayerAnimation animationController;

    //ボトルを生成する位置
    public Transform throwPoint;

    //投げるボトルのPrefab
    public GameObject bottlePrefab;

    //プレイヤーが向いている方向
    private int facingDirection = -1;

    //攻撃中かどうかを管理するフラグ
    //攻撃中は移動や追加攻撃をできないようにする
    private bool attacking;

    void Update()
    {
        //攻撃中ではない場合のみ操作を受け付ける
        if (!attacking)
        {
            //プレイヤー移動処理
            Move();

            //Enterキー入力でボトルを投げる
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ThrowBottle();
            }
        }
    }

    //プレイヤーの移動処理
    void Move()
    {
        //キーボード入力を取得
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //入力方向をVector2にまとめる
        Vector2 move = new Vector2(x, y);

        //入力がある場合
        if (move != Vector2.zero)
        {
            //移動方向を正規化して速度を一定にする
            //normalizedを使うことで斜め移動時の速度上昇を防ぐ
            transform.position += (Vector3)move.normalized * speed * Time.deltaTime;

            //歩行アニメーション開始
            animationController.SetWalking(true);

            //左方向へ移動した場合
            if (x < 0)
            {
                //キャラクターを左向きに反転
                transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

                //投げる方向を左に設定
                facingDirection = -1;
            }

            //右方向へ移動した場合
            else if (x > 0)
            {
                //キャラクターを右向きに反転
                transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);

                //投げる方向を右に設定
                facingDirection = 1;
            }
        }
        else
        {
            //入力がない場合は待機アニメーションへ
            animationController.SetWalking(false);
        }
    }

    //ボトルを投げる処理
    void ThrowBottle()
    {
        //投げるアニメーションを再生
        animationController.StartThrow();

        //throwPointの位置にボトルを生成
        GameObject bottle = Instantiate(bottlePrefab,throwPoint.position,Quaternion.identity);

        //生成したボトルの制御スクリプトを取得
        BottleController bottleScript = bottle.GetComponent<BottleController>();

        //ボトルを飛ばす方向
        Vector2 throwDirection;

        //プレイヤーの向いている方向によって投げる向きを決定
        if (facingDirection == 1)
        {
            //右向きの場合
            throwDirection = Vector2.right;
        }
        else
        {
            //左向きの場合
            throwDirection = Vector2.left;
        }

        //ボトル側のスクリプトへ投げる方向を渡す
        bottleScript.SetDirection(throwDirection);
    }
}