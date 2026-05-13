using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDirector : MonoBehaviour
{
    public Button[] buttons_;           //ボタン配列UI
    private int currentIndex_ = 0;      //現在選択中のボタン番号
    private bool isSwapped_ = false;    //キーが入れ替わっているかどうか

    void Start()
    {
        //ボタンが2つ以上ある場合のみ処理
        if (buttons_.Length > 0)
        {
            //最初のボタンを選択状態にする
            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);
        }
    }

    void Update()
    {
        //ボタンが存在しない場合は何もしない
        if (buttons_.Length == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(!isSwapped_)
            {
                //通常
                currentIndex_ = (currentIndex_ - 1 + buttons_.Length) % buttons_.Length;
            }
            else
            {
                //切り替え後
                currentIndex_ = (currentIndex_ + 1) % buttons_.Length;
            }

            //選択中のUIを更新
            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(!isSwapped_)
            {
                currentIndex_ = (currentIndex_ + 1) % buttons_.Length;
            }
            else
            {
                currentIndex_ = (currentIndex_ - 1 + buttons_.Length) % buttons_.Length;
            }

            EventSystem.current.SetSelectedGameObject(buttons_[currentIndex_].gameObject);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //フラグを反転
            isSwapped_ = !isSwapped_;
        }
    }
}