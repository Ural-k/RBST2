using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField] private float speed_;

    //プレイヤーアニメーションを制御するスクリプト
    [SerializeField] private PlayerAnimation animationController_;

    //ボトルを生成する位置
    [SerializeField] private Transform throwPoint_;

    //投げるボトルのPrefab
    [SerializeField] private GameObject bottlePrefab_;

    //狙う敵
    [SerializeField] private Transform enemyTarget_;

    //プレイヤーが初期で向いている方向
    private int facingDirection_ = -1;

    //攻撃中かどうかを管理するフラグ
    //攻撃中は移動や追加攻撃をできないようにする
    private bool isAttacking_;

    void Update()
    {
        //攻撃中ではない場合のみ操作を受け付ける
        if (!isAttacking_)
        {
            //プレイヤー移動処理
            Move();

            //マウス入力でボトルを投げる
            if (Input.GetMouseButtonDown(0))
            {
                ThrowBottle();
            }
            if (Input.GetMouseButtonDown(1))
            {
                ThrowBottle();
            }
            if (Input.GetMouseButtonDown(2))
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
            transform.position += (Vector3)move.normalized * speed_ * Time.deltaTime;

            //歩行アニメーション開始
            animationController_.SetWalking(true);

            //左方向へ移動した場合
            if (x < 0)
            {
                //キャラクターを左向きに反転
                transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                //投げる方向を左に設定
                facingDirection_ = -1;
            }

            //右方向へ移動した場合
            else if (x > 0)
            {
                //キャラクターを右向きに反転
                transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

                //投げる方向を右に設定
                facingDirection_ = 1;
            }
        }
        else
        {
            //入力がない場合は待機アニメーションへ
            animationController_.SetWalking(false);
        }
    }

    //ボトルを投げる処理
    void ThrowBottle()
    {
        //投げるアニメーションを再生
        animationController_.StartThrow();

        //throwPointの位置にボトルを生成
        GameObject bottle = Instantiate(bottlePrefab_,throwPoint_.position,Quaternion.identity);

        //生成したボトルの制御スクリプトを取得
        BottleController bottleScript = bottle.GetComponent<BottleController>();

        //ボトルを飛ばす方向
        Vector2 throwDirection;

        //敵が存在する場合
        if (enemyTarget_ != null)
        {
            //プレイヤーから敵への方向
            Vector2 toEnemy = (enemyTarget_.position - transform.position).normalized;

            //プレイヤーの向いている方向
            Vector2 forward = new Vector2(facingDirection_, 0);

            //敵が前側にいるか確認
            float directionDot = Vector2.Dot(forward, toEnemy);

            if (directionDot > 0)
            {
                //敵が前にいる場合は敵へ投げる
                throwDirection = toEnemy;
            }
            else
            {
                //敵が後ろにいる場合は正面へ投げる
                throwDirection = forward;
            }
        }
        else
        {
            //敵がいない場合は正面へ投げる
            throwDirection = new Vector2(facingDirection_, 0);
        }

        //ボトル側のスクリプトへ投げる方向を渡す
        bottleScript.SetDirection(throwDirection);
    }
}
