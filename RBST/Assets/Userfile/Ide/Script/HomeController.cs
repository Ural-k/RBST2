using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    [SerializeField] private Image courseImage_;           //コース判定用のUI
    [SerializeField] private Image kyaraImage_;            //キャラ判定用のUI
    [SerializeField] private GameObject[] player_;         //UIエリアへの侵入判定を行うプレイヤー
    [SerializeField] private Canvas canvas_;               //courseImage_のCanvas参照(座標変換に使用)
    [SerializeField] private GameObject courseSelect_;     //コース選択UI
    [SerializeField] private GameObject kyaraSelect_;      //キャラ選択UI
    [SerializeField] private Text courseText_;             //コース案内テキスト
    [SerializeField] private Text kyaraText_;              //キャラ案内テキスト
    [SerializeField] private GameObject coursePanel_;      //コース選択画面のUIパネル
    [SerializeField] private GameObject kyaraPanel_;       //キャラクター選択画面のUIパネル

    private void Start()
    {
        //Imageが属しているCanvasを取得(スクリーン座標判定で使用)
        canvas_ = courseImage_.canvas;

        //初期状態では両方のUIパネルを表示状態にする
        courseSelect_.SetActive(true);
        kyaraSelect_.SetActive(true);

        //初期状態では案内テキストは非表示
        courseText_.gameObject.SetActive(false);
        kyaraText_.gameObject.SetActive(false);

        foreach (GameObject player in player_)
        {
            player.SetActive(false);
        }

        int index = GameManager.instance_.selectedCharacterIndex_;

        //安全チェック付きで表示
        if (index >= 0 && index < player_.Length)
        {
            player_[index].SetActive(true);
        }

        //配列内のすべてのキューブを確認し、
        //選択中のインデックスと一致するものだけ表示する
        for (int i = 0; i < player_.Length; i++)
        {
            player_[i].SetActive(i == index);
        }
    }

    private void Update()
    {
        //プレイヤーがコースエリア内にいるかどうか
        bool isInCourse = false;

        //プレイヤーがキャラエリア内にいるかどうか
        bool isInKyara = false;

        //全プレイヤーを対象に判定を行う
        foreach (GameObject player in player_)
        {
            //ワールド座標 → スクリーン座標へ変換
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);

            //コースUI領域内にいるか判定
            if (RectTransformUtility.RectangleContainsScreenPoint
            (
                    courseImage_.rectTransform,
                    screenPoint,
                    canvas_.renderMode == RenderMode.ScreenSpaceOverlay
                    ? null
                    : Camera.main
            ))
            {
                isInCourse = true;
            }

            //キャラUI領域内にいるか判定
            if (RectTransformUtility.RectangleContainsScreenPoint
            (
                    kyaraImage_.rectTransform,
                    screenPoint,
                    canvas_.renderMode == RenderMode.ScreenSpaceOverlay
                    ? null
                    : Camera.main
            ))
            {
                isInKyara = true;
            }
        }

        //コースエリアに入っている場合のみテキスト表示
        courseText_.gameObject.SetActive(isInCourse);

        //キャラエリアに入っている場合のみテキスト表示
        kyaraText_.gameObject.SetActive(isInKyara);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isInKyara)
            {
                GameManager.instance_.ShowPanel();

                for(int i = 0; i < player_.Length; i++)
                {
                    player_[i].SetActive(false);
                }
            }
        }
    }
}