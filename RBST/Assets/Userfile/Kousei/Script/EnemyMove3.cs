using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMove3 : MonoBehaviour
    {
        //Inspector設定

        [Header("出現範囲(最小座標)")]
        [SerializeField]
        private Vector2 minPosition_;

        [Header("出現範囲(最大座標")]
        [SerializeField]
        private Vector2 maxPosition_;

        [Header("テレポート間隔(秒)")]
        [SerializeField]
        private float teleportInterval_ = 3f;

        //内部変数
        private float timer_ = 0f;

        //初期化
        private void Start()
        {
            //ゲーム開発時に一度ランダム位置へ移動
            Teleport();       
         }

        //メイン処理
        private void UpdateTeleportTimer()
        {
            timer_ += Time.deltaTime;

            bool isTimeOver = timer_ >= teleportInterval_;

            if (isTimeOver)
            {
                Teleport();
                timer_ = 0f;
            }
        }

        //テレポート処理
        private void Teleport()
        {
            float randomX =
                Random.Range(
                    minPosition_.x,
                    maxPosition_.x
                    );

            float randomY =
                Random.Range(
                    minPosition_.y,
                    maxPosition_.y
                    );

            Vector2 randomPosition =
                new Vector2(
                    randomX,
                    randomY
                    );

            transform.position = randomPosition;
        }

        private void Update()
        {
            UpdateTeleportTimer();
        }

    }
}
