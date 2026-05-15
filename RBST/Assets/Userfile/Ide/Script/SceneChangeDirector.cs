using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeDirector : MonoBehaviour
{
    void Start()
    {
        //1秒後にシーンを切り替える
        Invoke("SceneChange", 1f);
    }

    void SceneChange()
    {
        //切り替え先のシーン名を指定
        SceneManager.LoadScene("AfterScene");
    }
}