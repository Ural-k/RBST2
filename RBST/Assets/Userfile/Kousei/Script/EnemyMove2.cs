using UnityEngine;

namespace Game.Enemy

{

    public class EnemyMove2 : MonoBehaviour

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

        [Header("表示時間（秒）")]

        [SerializeField]

        private float visibleTime_ = 3f;

        [Header("非表示時間（秒）")]

        [SerializeField]

        private float invisibleTime_ = 2f;

        //内部変数

        private int currentPointIndex_ = 0;

        private float waitTimer_ = 0f;

        private bool isWaiting_ = false;

        // 透明制御

        private float visibleTimer_ = 0f;

        private bool isVisible_ = true;

        private SpriteRenderer spriteRenderer_;

        private void Awake()

        {

            spriteRenderer_ = GetComponent<SpriteRenderer>();

        }

        //メイン処理

        private void Update()

        {

            if (points_ == null || points_.Length == 0)

            {

                return;

            }

            UpdateVisibility();

            if (isWaiting_)

            {

                UpdateWaiting();

                return;

            }

            MoveToPoint();

        }

        //============================

        // 透明制御

        //============================

        private void UpdateVisibility()

        {

            visibleTimer_ += Time.deltaTime;

            if (isVisible_)

            {

                bool isSwitchInvisible = visibleTimer_ >= visibleTime_;

                if (isSwitchInvisible)

                {

                    SetInvisible();

                }

            }

            else

            {

                bool isSwitchVisible = visibleTimer_ >= invisibleTime_;

                if (isSwitchVisible)

                {

                    SetVisible();

                }

            }

        }

        private void SetVisible()

        {

            isVisible_ = true;

            visibleTimer_ = 0f;

            Color color = spriteRenderer_.color;

            color.a = 1f;

            spriteRenderer_.color = color;

        }

        private void SetInvisible()

        {

            isVisible_ = false;

            visibleTimer_ = 0f;

            Color color = spriteRenderer_.color;

            color.a = 0f;

            spriteRenderer_.color = color;

        }

        //============================

        // 移動処理（そのまま）

        //============================

        private void MoveToPoint()

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

        private void StartWaiting()

        {

            isWaiting_ = true;

            waitTimer_ = 0f;

        }

        private void UpdateWaiting()

        {

            waitTimer_ += Time.deltaTime;

            bool isWaitFinished = waitTimer_ >= waitTime_;

            if (isWaitFinished)

            {

                EndWaiting();

            }

        }

        private void EndWaiting()

        {

            isWaiting_ = false;

            UpdateNextPointIndex();

        }

        private void UpdateNextPointIndex()

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
