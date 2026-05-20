using UnityEngine;

namespace Game.Enemy

{

    // 敵をランダムな位置へ滑らかに移動させるクラス
    [RequireComponent(typeof(Rigidbody2D))]

    public class EnemyMove4 : MonoBehaviour

    {

        // フルHD基準のステージサイズ
        private const float SCREEN_WIDTH = 19.2f;

        private const float SCREEN_HEIGHT = 10.8f;

        [SerializeField]

        [Tooltip("敵の移動速度")]

        private float moveSpeed_ = 3.0f;

        [SerializeField]

        [Tooltip("目的地を変更する時間")]

        private float changeTargetInterval_ = 2.0f;

        // Rigidbody2Dの参照
        private Rigidbody2D enemyRigidbody2D_;

        // 現在の移動目標地点
        private Vector2 targetPosition_;

        // 経過時間
        private float elapsedTime_;

        // 初期化
        private void Awake()

        {

            // Rigidbody2Dを取得
            enemyRigidbody2D_ = GetComponent<Rigidbody2D>();

            // 初回の目的地を設定
            SetRandomTargetPosition();

        }

        // 毎フレーム更新
        private void Update()

        {

            // 経過時間を加算
            elapsedTime_ += Time.deltaTime;

            // 目的地変更時間を超えたか判定
            bool hasReachedInterval

                = elapsedTime_ >= changeTargetInterval_;

            if (!hasReachedInterval)

            {

                return;

            }

            // タイマーをリセット
            elapsedTime_ = 0.0f;

            // 新しい目的地を設定
            SetRandomTargetPosition();

        }

        // 物理更新
        private void FixedUpdate()

        {

            Move();

        }

        // 敵を目標地点へ移動させる
        private void Move()

        {

            // 現在位置を取得
            Vector2 currentPosition

                = enemyRigidbody2D_.position;

            // 現在位置から目標地点へ滑らかに移動
            Vector2 nextPosition

                = Vector2.Lerp

                (

                    currentPosition,

                    targetPosition_,

                    moveSpeed_ * Time.fixedDeltaTime

                );

            // Rigidbody2Dで移動
            enemyRigidbody2D_.MovePosition(nextPosition);

        }

        // ランダムな目的地を設定
        private void SetRandomTargetPosition()

        {

            // ランダムなX座標を生成
            float randomX

                = Random.Range

                (

                    -SCREEN_WIDTH * 0.5f,

                    SCREEN_WIDTH * 0.5f

                );

            // ランダムなY座標を生成
            float randomY

                = Random.Range

                (

                    -SCREEN_HEIGHT * 0.5f,

                    SCREEN_HEIGHT * 0.5f

                );

            // 目標地点を更新
            targetPosition_

                = new Vector2(randomX, randomY);

        }

    }

}
