using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMove2 : MonoBehaviour
    {
        // ポイント到達とみなす距離
        private const float ARRIVE_DISTANCE = 0.1f;

        // Inspector設定

        [Header("移動するポイント")]
        // 移動する経路ポイント
        [SerializeField]
        private Transform[] points_;

        [Header("移動スピード")]
        // 移動速度
        [SerializeField]
        private float moveSpeed_ = 2f;

        [Header("停止時間（秒）")]
        // ポイント到達後の待機時間
        [SerializeField]
        private float waitTime_ = 2f;

        [Header("表示時間（秒）")]
        // 表示状態の継続時間
        [SerializeField]
        private float visibleTime_ = 3f;

        [Header("非表示時間（秒）")]
        // 非表示状態の継続時間
        [SerializeField]
        private float invisibleTime_ = 2f;

        // 現在向かっているポイントのインデックス
        private int currentPointIndex_ = 0;

        // 待機時間カウント用タイマー
        private float waitTimer_ = 0f;

        // 待機中かどうか
        private bool isWaiting_ = false;

        // 表示/非表示の経過時間
        private float visibleTimer_ = 0f;

        // 現在表示状態かどうか
        private bool isVisible_ = true;

        // スプライト描画用コンポーネント
        private SpriteRenderer spriteRenderer_;

        private void Awake()
        {
            // SpriteRendererを取得
            spriteRenderer_ = GetComponent<SpriteRenderer>();
        }

        // メイン処理
        private void Update()
        {
            // ポイント未設定なら何もしない
            if (points_ == null || points_.Length == 0)
            {
                return;
            }

            // 表示・非表示の切り替え更新
            UpdateVisibility();

            // 待機中なら移動せず待機処理のみ
            if (isWaiting_)
            {
                UpdateWaiting();
                return;
            }

            // 通常移動処理
            MoveToPoint();
        }

        private void UpdateVisibility()
        {
            // 経過時間を加算
            visibleTimer_ += Time.deltaTime;

            if (isVisible_)
            {
                // 表示時間を超えたら非表示へ
                bool isSwitchInvisible = visibleTimer_ >= visibleTime_;
                if (isSwitchInvisible)
                {
                    SetInvisible();
                }
            }
            else
            {
                // 非表示時間を超えたら表示へ
                bool isSwitchVisible = visibleTimer_ >= invisibleTime_;
                if (isSwitchVisible)
                {
                    SetVisible();
                }
            }
        }

        private void SetVisible()
        {
            // 表示状態に切り替え
            isVisible_ = true;

            // タイマーリセット
            visibleTimer_ = 0f;

            // アルファ値を1（完全表示）にする
            Color color = spriteRenderer_.color;
            color.a = 1f;
            spriteRenderer_.color = color;
        }

        private void SetInvisible()
        {
            // 非表示状態に切り替え
            isVisible_ = false;

            // タイマーリセット
            visibleTimer_ = 0f;

            // アルファ値を0（透明）にする
            Color color = spriteRenderer_.color;
            color.a = 0f;
            spriteRenderer_.color = color;
        }

        private void MoveToPoint()
        {
            // 現在のターゲットポイント取得
            Transform targetPoint = points_[currentPointIndex_];

            // ターゲットへ向かって移動
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed_ * Time.deltaTime
            );

            // ターゲットとの距離を計算
            float distance =
                Vector2.Distance(
                    transform.position,
                    targetPoint.position
                );

            // 到達判定
            bool isArrived = distance < ARRIVE_DISTANCE;
            if (isArrived)
            {
                StartWaiting();
            }
        }

        private void StartWaiting()
        {
            // 待機開始
            isWaiting_ = true;
            waitTimer_ = 0f;
        }

        private void UpdateWaiting()
        {
            // 待機時間を加算
            waitTimer_ += Time.deltaTime;

            // 待機時間終了チェック
            bool isWaitFinished = waitTimer_ >= waitTime_;
            if (isWaitFinished)
            {
                EndWaiting();
            }
        }

        private void EndWaiting()
        {
            // 待機終了
            isWaiting_ = false;

            // 次のポイントへ
            UpdateNextPointIndex();
        }

        private void UpdateNextPointIndex()
        {
            // インデックスを進める
            currentPointIndex_++;

            // 配列を超えたら最初に戻る（ループ）
            bool isOverIndex = currentPointIndex_ >= points_.Length;
            if (isOverIndex)
            {
                currentPointIndex_ = 0;
            }
        }
    }
}
