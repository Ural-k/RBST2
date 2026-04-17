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
        
        //内部変数
        private int currentPointIndex_ = 0;

        //メイン処理
        private void Update()
        {
            // ポイントが未設定なら処理しない（早期リターン）
            if (points_ == null || points_.Length == 0)
            {
                return;
            }

            MoveToPoint();
        }

        //移動処理

        private void MoveToPoint()
        {
            Transform targetPoint = points_[currentPointIndex_];

            // 現在位置 → 目標位置へ移動
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed_ * Time.deltaTime
            );

            // 到達判定
            float distance =
                Vector2.Distance(
                    transform.position,
                    targetPoint.position
                );

            bool isArrived = distance < ARRIVE_DISTANCE;

            if (isArrived)
            {
                UpdateNextPointIndex();
            }
        }

        //次のポイントへ

        private void UpdateNextPointIndex()
        {
            currentPointIndex_++;

            // 最後まで行ったら最初に戻る
            bool isOverIndex = currentPointIndex_ >= points_.Length;

            if (isOverIndex)
            {
                currentPointIndex_ = 0;
            }
        }
    }
}