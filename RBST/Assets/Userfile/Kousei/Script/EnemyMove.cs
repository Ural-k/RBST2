using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        //定数
        private const float ARRIVE_DISTANCE = 0.1f;

        //Inspector設定
        [Header("移動するポイント")]
        [SerializeField]
        private Transform[] points_;

        [Header("移動スピード")]
        [SerializeField]
        private float moveSpeed_ = 2f;

        [Header("停止時間（秒）")]
        [SerializeField]
        private float waitTime_ = 2f;

        //内部変数
        private int currentPointIndex_ = 0;
        private float waitTimer_ = 0f;

        // 停止中かどうか
        private bool isWaiting_ = false;

        //メイン処理
        private void Update()
        {
            // ポイント未設定なら何もしない
            if (points_ == null || points_.Length == 0)
            {
                return;
            }

            // 停止中かどうかで処理を分ける
            if (isWaiting_)
            {
                UpdateWaiting();
                return;
            }

            //MoveToPoint();
        }

        //移動処理
        public void UpdatePoint(Vector2 nextPoint)
        {
            Transform targetPoint = points_[currentPointIndex_];

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed_ * Time.deltaTime
            );

            float distance =
                Vector2.Distance(
                    transform.position,
                    targetPoint.position
                );

            bool isArrived = distance < ARRIVE_DISTANCE;

            if (isArrived)
            {
                StartWaiting();
            }
        }

        //停止開始
        public void StartWaiting()
        {
            isWaiting_ = true;
            waitTimer_ = 0f;
        }

        //停止中処理
        public void UpdateWaiting()
        {
            waitTimer_ += Time.deltaTime;

            bool isWaitFinished = waitTimer_ >= waitTime_;

            if (isWaitFinished)
            {
                EndWaiting();
            }
        }

        //停止終了
        public void EndWaiting()
        {
            isWaiting_ = false;
            SetNextPoint();
        }

        //次のポイントへ
        private void SetNextPoint()
        {
            currentPointIndex_++;

            bool isOverIndex = currentPointIndex_ >= points_.Length;

            if (isOverIndex)
            {
                currentPointIndex_ = 0;
            }
        }
    }
}