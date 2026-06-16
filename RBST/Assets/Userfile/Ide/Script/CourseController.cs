using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CourseController : MonoBehaviour
{
    [SerializeField] private Button[] buttons_;        //ホームボタン配列
    [SerializeField] private int columnCount_;         //横方向のボタン数
    [SerializeField] private int currentIndex_;        //現在選択中のボタン番号

    void Start()
    {
        //ボタンが1つ以上ある場合のみ初期選択を設定
        if (buttons_.Length > 0)
        {
            //現在選択中のUIを更新する
            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);
        }
    }

    void Update()
    {
        //ボタンが未設定の場合は処理しない
        if (buttons_.Length == 0)
        {
            return;
        }

        //現在の行番号
        int rowIndex = currentIndex_ / columnCount_;

        //現在の列番号
        int columnIndex = currentIndex_ % columnCount_;

        //最下段の行番号
        int maxRowIndex = (buttons_.Length - 1) / columnCount_;

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    rowIndex--;

        //    //上端を超えたら最下段へループ
        //    if (rowIndex < 0)
        //    {
        //        rowIndex = maxRowIndex;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    rowIndex++;

        //    //下端を超えたら最上段へループ
        //    if (rowIndex > maxRowIndex)
        //    {
        //        rowIndex = 0;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            columnIndex--;

            //左端を超えたら右端へループ
            if (columnIndex < 0)
            {
                columnIndex = columnCount_ - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            columnIndex++;

            //右端を超えたら左端へループ
            if (columnIndex >= columnCount_)
            {
                columnIndex = 0;
            }
        }

        //行・列 → 配列インデックスへ変換
        int newIndex = rowIndex * columnCount_ + columnIndex;

        //範囲チェックをして存在しないボタンは無視する
        if (newIndex < buttons_.Length)
        {
            currentIndex_ = newIndex;
            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);
        }
    }
}