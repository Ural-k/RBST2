using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
    List<Transform> buttons_;
    public void Navigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 navigate = context.ReadValue<Vector2>();
        if (Mathf.Abs(navigate.x) <= Mathf.Abs(navigate.y))//上下入力
        {
            buttons_.Select(n => n.transform.position.y > EventSystem.current.currentSelectedGameObject.transform.position.y);
            buttons_.OrderBy(n => Vector2.Distance(transform.position, n.position)).First();
        }
        else
        {
            
        }


    }

    /// <summary>
    /// 選択するボタンを変更する処理(引数から選択するボタンを取得)
    /// </summary>
    /// <param name="button"></param>
    void ChangeSelectButton(GameObject button)
    {
        //選択するボタンを変更
        EventSystem.current.SetSelectedGameObject(button);
    }
}
