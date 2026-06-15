using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    /// マウスポインターが重なった時に
    /// ボタンを少し上へ移動させるコンポーネント
    public class ButtonHoverAnimation : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        //定数
        //補間速度
        private const float MOVE_SPEED = 10.0f;

        //Inspector設定

        [Header("ホバー時の移動量")]
        [SerializeField]
        private float hoverMoveAmount_ = 10.0f;

        //メンバ変数
        /// 元の座標
        private Vector3 defaultPosition_;

        /// 目標座標
        private Vector3 targetPosition_;

        /// マウスが乗っているか
        private bool isHover_;

        /// RectTransform
  
        private RectTransform rectTransform_;

        //Unityイベント

        private void Awake()
        {
            rectTransform_ = GetComponent<RectTransform>();

            defaultPosition_
                = rectTransform_.anchoredPosition;

            targetPosition_
                = defaultPosition_;
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
        }

        //ポインターイベント

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
    }
}
