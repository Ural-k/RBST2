using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 敵などをターゲットする
/// </summary>
public class TargetToEnemy : MonoBehaviour
{
    [SerializeField,
    Header("ターゲット可能なオブジェクトの親オブジェクト")] GameObject targetersObject_;
    [SerializeField,
    Header("ターゲット目印UI(GameObject)")]             Transform targetGraphic_;

    /*現在のターゲット*/
    Transform nowTarget_ = null;

    public Transform targetTransform { get { return nowTarget_; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Target(); //※仮、Tキーでターゲット切り替え
    }

    /// <summary>
    /// 敵をターゲットする
    /// </summary>
    void Target()
    {
        //全ターゲット対象をList化
        List<Transform> unintentionalTargeter = new();
        for(int i = 0; i < targetersObject_.transform.childCount; ++i)
            unintentionalTargeter.Add(targetersObject_.transform.GetChild(i));

        //タゲ対象を左上優先で順番にList化
        var sortTargeter = 
            unintentionalTargeter.OrderBy(n => n.position.x).ThenByDescending(n => n.position.y).ToList();

        //次項へターゲット
        if(nowTarget_ == null || nowTarget_ == sortTargeter[sortTargeter.Count - 1])
            nowTarget_ = sortTargeter.First();
        else
        {
            for(int i = 0; i < sortTargeter.Count - 1; ++i)
            {
                if (nowTarget_ == sortTargeter[i])
                {
                    nowTarget_ = sortTargeter[i + 1];
                    break;
                }
            }
        }

        //ターゲットUI操作
        if (targetGraphic_ == null) return;
        targetGraphic_.parent = nowTarget_;
        targetGraphic_.localPosition = Vector2.zero;
    }
}

//被攻撃用スクリプト(Enemy用,Player用←interface)
//マウスカーソル型アクション