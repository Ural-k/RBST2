using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    /// <summary>
    /// ボタンのホバーアニメーション
    /// ・マウスが乗ると上に移動
    /// ・クリックすると少し縮む
    /// </summary>
    public class ButtonHoverAnimation : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        // ===== 定数 =====

        private const float MOVE_SPEED = 10.0f;
        private const float SCALE_SPEED = 15.0f;

        // ===== Inspector設定 =====

        [Header("ホバー時の移動量")]
        [SerializeField]
        private float hoverMoveAmount_ = 10.0f;

        [Header("クリック時の縮小率")]
        [SerializeField]
        private float pressedScale_ = 0.9f;

        // ===== メンバ変数 =====

        private RectTransform rectTransform_;

        private Vector3 defaultPosition_;
        private Vector3 targetPosition_;

        private Vector3 defaultScale_;
        private Vector3 targetScale_;

        private bool isHover_;

        // ===== Unityイベント =====

        private void Awake()
        {
            rectTransform_ = GetComponent<RectTransform>();

            defaultPosition_
                = rectTransform_.anchoredPosition;

            targetPosition_
                = defaultPosition_;

            defaultScale_
                = transform.localScale;

            targetScale_
                = defaultScale_;
        }

        private void Update()
        {
            rectTransform_.anchoredPosition
                = Vector3.Lerp
                (
                    rectTransform_.anchoredPosition,
                    targetPosition_,
                    Time.deltaTime * MOVE_SPEED
                );

            transform.localScale
                = Vector3.Lerp
                (
                    transform.localScale,
                    targetScale_,
                    Time.deltaTime * SCALE_SPEED
                );
        }

        // ===== ホバー =====

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHover_ = true;

            targetPosition_
                = defaultPosition_
                + Vector3.up * hoverMoveAmount_;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHover_ = false;

            targetPosition_
                = defaultPosition_;
        }

        // ===== クリック =====

        public void OnPointerDown(PointerEventData eventData)
        {
            targetScale_
                = defaultScale_ * pressedScale_;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            targetScale_
                = defaultScale_;
        }
    }
}