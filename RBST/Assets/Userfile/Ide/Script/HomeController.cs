using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    [SerializeField] private Image kyaraImage_;            //キャラ判定用のUI
    [SerializeField] public GameObject[] player_;          //UIエリアへの侵入判定を行うプレイヤー
    [SerializeField] private GameObject kyaraSelect_;      //キャラ選択UI
    [SerializeField] private Text kyaraText_;              //キャラ案内テキスト
    [SerializeField] private GameObject kyaraPanel_;       //キャラクター選択画面のUIパネル
    [SerializeField] private Canvas canvas_;               //HomePanel用のCanvas

    private void Start()
    {
        //初期状態ではキャラセレクトのUIパネルを表示状態にする
        kyaraSelect_.SetActive(true);

        //初期状態では案内テキストは非表示
        kyaraText_.gameObject.SetActive(false);

        //すべてのキャラクターを一旦非表示にする
        foreach (GameObject players in player_)
        {
            players.SetActive(false);
        }

        //GameManagerで保存されている選択キャラクター番号を取得
        int index = GameManager.instance_.selectedCharacterIndex_;

        //安全チェック付きで表示
        if (index >= 0 && index < player_.Length)
        {
            player_[index].SetActive(true);
        }

        //配列内のすべてのキューブを確認し、選択中のインデックスと一致するものだけ表示する
        for (int i = 0; i < player_.Length; i++)
        {
            player_[i].SetActive(i == index);
        }
    }

    private void Update()
    {
        //プレイヤーがキャラエリア内にいるかどうか
        bool isInKyara = false;

        //全プレイヤーを対象に判定を行う
        foreach (GameObject player in player_)
        {
            //Canvasで使用しているカメラを取得
            Camera cam = canvas_.worldCamera;

            //ワールド座標 → スクリーン座標へ変換
            Vector2 screenPoint = cam.WorldToScreenPoint(player.transform.position);

            //キャラUI領域内にいるか判定
            if (RectTransformUtility.RectangleContainsScreenPoint(kyaraImage_.rectTransform,screenPoint,cam))
            {
                isInKyara = true;
            }
        }

        //キャラエリアに入っている場合のみテキスト表示
        kyaraText_.gameObject.SetActive(isInKyara);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //プレイヤーがエリア内にいる場合のみ実行
            if (isInKyara)
            {
                //キャラクター選択画面へ遷移
                GameManager.instance_.ShowPanel();

                //全キャラクターを初期位置へ戻し非表示にする
                for (int i = 0; i < player_.Length; i++)
                {
                    player_[i].transform.position = new Vector3(1, 0, 0);
                    player_[i].SetActive(false);
                }
            }
        }
    }
}