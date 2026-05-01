using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMove3 : MonoBehaviour
    {
        //Inspector設定
        [Header("出現範囲（最小座標）")]
        [SerializeField]
        private Vector2 minPosition_;

        [Header("出現範囲（最大座標）")]
        [SerializeField]
        private Vector2 maxPosition_;

        [Header("テレポート間隔（秒）")]
        [SerializeField]
        private float teleportInterval_ = 3f;

        [Header("フェード時間（秒）")]
        [SerializeField]
        private float fadeDuration_ = 0.5f;

        // ====== 内部変数
        private float timer_ = 0f;
        private float fadeTimer_ = 0f;

        private bool isFadingOut_ = false;
        private bool isFadingIn_ = false;

        private SpriteRenderer spriteRenderer_;

        //初期化
        private void Start()
        {
            spriteRenderer_ = GetComponent<SpriteRenderer>();
            Teleport();
        }

        //メイン処理
        private void Update()
        {
            // フェード中は優先処理
            if (isFadingOut_)
            {
                UpdateFadeOut();
                return;
            }

            if (isFadingIn_)
            {
                UpdateFadeIn();
                return;
            }

            UpdateTeleportTimer();
        }

        //タイマー更新
        private void UpdateTeleportTimer()
        {
            timer_ += Time.deltaTime;

            bool isTimeOver = timer_ >= teleportInterval_;

            if (isTimeOver)
            {
                StartFadeOut();
                timer_ = 0f;
            }
        }

        //フェードアウト開始
        private void StartFadeOut()
        {
            isFadingOut_ = true;
            fadeTimer_ = 0f;
        }

        //フェードアウト処理
        private void UpdateFadeOut()
        {
            fadeTimer_ += Time.deltaTime;

            float alpha = 1f - (fadeTimer_ / fadeDuration_);

            SetAlpha(alpha);

            bool isFadeFinished = fadeTimer_ >= fadeDuration_;

            if (isFadeFinished)
            {
                isFadingOut_ = false;

                Teleport();

                StartFadeIn();
            }
        }

        //フェードイン開始
        private void StartFadeIn()
        {
            isFadingIn_ = true;
            fadeTimer_ = 0f;
        }

        //フェードイン処理
        private void UpdateFadeIn()
        {
            fadeTimer_ += Time.deltaTime;

            float alpha = fadeTimer_ / fadeDuration_;

            SetAlpha(alpha);

            bool isFadeFinished = fadeTimer_ >= fadeDuration_;

            if (isFadeFinished)
            {
                isFadingIn_ = false;
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

        //透明度設定
        private void SetAlpha(float alpha)
        {
            Color color = spriteRenderer_.color;
            color.a = alpha;
            spriteRenderer_.color = color;
        }
    }
}