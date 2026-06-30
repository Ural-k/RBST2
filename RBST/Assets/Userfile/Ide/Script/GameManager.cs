using UnityEngine;

public class GameManager : MonoBehaviour
{
    //キャラ選択UIパネル
    [SerializeField] private GameObject kyaraPanel_;

    //キャラ選択の操作UI(ボタン操作などを担当するオブジェクト)
    [SerializeField] private GameObject kyaraSelectController_;

    //ホーム画面UIパネル
    [SerializeField] private GameObject homePanel_;

    //ホーム画面の制御スクリプト(表示制御用オブジェクト)
    [SerializeField] private GameObject homeController_;

    //シングルトンインスタンス(どこからでもアクセス可能)
    public static GameManager instance_ { get; private set; }

    //現在選択されているキャラクターのインデックス
    //UI選択やホーム画面の表示切り替えに使用される
    [SerializeField] public int selectedCharacterIndex_;

    //オブジェクト生成時に呼ばれる
    //シングルトンインスタンスを設定する
    private void Awake()
    {
        instance_ = this;
    }

    private void Start()
    {
        //ゲーム開始時はキャラ選択画面を表示状態にする
        kyaraPanel_.SetActive(true);
        kyaraSelectController_.SetActive(true);

        //ホーム画面は非表示状態で開始する
        homePanel_.SetActive(false);
        homeController_.SetActive(false);
    }

    //キャラ選択画面を表示状態にする
    public void ShowPanel()
    {
        kyaraPanel_.SetActive(true);
        kyaraSelectController_.SetActive(true);

        homePanel_.SetActive(false);
        homeController_.SetActive(false);
    }

    //ホーム画面を表示状態にする
    public void HidePanel()
    {
        kyaraPanel_.SetActive(false);
        kyaraSelectController_.SetActive(false);

        //ホーム画面制御スクリプトを取得
        HomeController homeControllers = homeController_.GetComponent<HomeController>();

        //選択されたキャラクターを表示
        homeControllers.player_[selectedCharacterIndex_].SetActive(true);

        homePanel_.SetActive(true);
        homeController_.SetActive(true);
    }

    //キャラ選択とホーム画面の表示を切り替える
    //UI遷移用の簡易切り替え処理
    public void TogglePanel()
    {
        kyaraPanel_.SetActive(!kyaraPanel_.activeSelf);
        kyaraSelectController_.SetActive(!kyaraSelectController_.activeSelf);

        homePanel_.SetActive(!homePanel_.activeSelf);
        homeController_.SetActive(!homeController_.activeSelf);
    }
}