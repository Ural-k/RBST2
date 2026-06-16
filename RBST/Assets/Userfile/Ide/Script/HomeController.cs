using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    //コース選択UIパネル
    [SerializeField] private GameObject courseSelect_;

    //キャラ選択UIパネル
    [SerializeField] private GameObject kyaraSelect_;

    //コース案内テキスト
    [SerializeField] private Text courseText_;

    //キャラ案内テキスト
    [SerializeField] private Text kyaraText_;

    //キャラクター選択に応じて表示されるキューブ(色・キャラ表現用)
    [SerializeField] private GameObject[] cube_;

    //前回表示していたキャラインデックス(変更検知用)
    private int lastIndex = -1;

    private void Start()
    {
        //初期状態では両方のUIパネルを表示状態にする
        courseSelect_.SetActive(true);
        kyaraSelect_.SetActive(true);

        //初期状態では案内テキストは非表示
        courseText_.gameObject.SetActive(false);
        kyaraText_.gameObject.SetActive(false);

        //現在の選択キャラインデックスを取得
        int index = GameManager.instance_.selectedCharacterIndex_;

        // 初期状態の選択が前回値と異なる場合のみ更新処理を実行(初回表示時の安全な更新処理)
        if (index != lastIndex)
        {
            lastIndex = index;
            ShowCube();
        }
    }

    //選択されているキャラに応じてキューブ表示を切り替える
    private void ShowCube()
    {
        int index = GameManager.instance_.selectedCharacterIndex_;

        //配列内のすべてのキューブを確認し、
        //選択中のインデックスと一致するものだけ表示する
        for (int i = 0; i < cube_.Length; i++)
        {
            cube_[i].SetActive(i == index);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            courseText_.gameObject.SetActive(true);
            kyaraText_.gameObject.SetActive(true);
        }
    }
}