using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scene
{
    /// UIボタンを押した時にシーンを切り替えるクラス
    public class ChangGameScene : MonoBehaviour
    {
        // Inspector設定
        [Header("遷移先シーン名")]

        // ボタン押下時に移動するシーン名
        // Build Settingsに登録されているシーン名を入力する
        [SerializeField]
        private string nextSceneName_;

        /// ボタンが押された時に呼び出す
        public void ChangeScene()
        {
            // シーン名が入力されているか確認する
            bool hasSceneName
                = string.IsNullOrEmpty(nextSceneName_) == false;

            // シーン名が入力されていない場合
            if (hasSceneName == false)
            {
                // エラーメッセージを表示する
                Debug.LogError("遷移先のシーン名が設定されていません。");

                // 処理を終了する
                return;
            }

            // 指定されたシーンへ移動する
            SceneManager.LoadScene(nextSceneName_);
        }
    }
}