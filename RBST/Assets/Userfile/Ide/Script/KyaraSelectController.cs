using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KyaraSelectController : MonoBehaviour
{
    [SerializeField] private Button[] buttons_;        //キャラボタン配列
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

        //現在位置を「グリッド座標」に変換
        int rowIndex = currentIndex_ / columnCount_;        //行
        int columnIndex = currentIndex_ % columnCount_;     //列

        //一番下の行のインデックス(範囲チェックや拡張用)
        int maxRowIndex = (buttons_.Length - 1) / columnCount_;

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

        //グリッド座標(行・列)を配列インデックスへ変換
        int newIndex = rowIndex * columnCount_ + columnIndex;

        //範囲チェックをして存在しないボタンは無視する
        if (newIndex < buttons_.Length)
        {
            //選択状態を更新
            currentIndex_ = newIndex;

            //UIのフォーカスを更新(見た目の選択状態を同期)
            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);

            //グローバル状態(GameManager)に選択キャラを反映
            GameManager.instance_.selectedCharacterIndex_ = currentIndex_;
        }
    }

    //UIボタンのクリック(マウス選択)時に呼ばれる処理
    public void OnSelect(int index)
    {
        //選択インデックスを更新
        currentIndex_ = index;

        //UIフォーカスを更新(キーボード操作と状態を統一)
        EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);

        //ゲーム全体の選択状態を更新
        GameManager.instance_.selectedCharacterIndex_ = currentIndex_;
    }
}