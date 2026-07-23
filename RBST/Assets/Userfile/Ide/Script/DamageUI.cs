using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    //生成するダメージ表示用TextのPrefab
    [SerializeField] private GameObject damageUI;

    //ダメージ表示を配置するCanvas
    [SerializeField] private Canvas popupCanvas;

    //ダメージ表示を追従させる対象(敵やプレイヤー)
    [SerializeField] private Transform targetObject;

    //テスト時に使用する初期ダメージ量
    [SerializeField] private int defaultDamage;

    //現在表示中のダメージText
    //nullの場合は現在表示されていない
    private GameObject currentPopup;

    //現在表示中の合計ダメージ量
    //短時間に複数回攻撃を受けた場合、この値へ加算する
    private int accumulatedDamage = 0;

    //ダメージTextの移動・消滅処理を管理するCoroutine
    private Coroutine currentCoroutine;

    //動作確認用
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(defaultDamage);
        }
    }

    //ダメージを受けた時に呼び出す処理
    public void TakeDamage(int damage = -1)
    {
        //引数が指定されていない場合は設定した初期ダメージを使用
        int actualDamage = (damage < 0) ? defaultDamage : damage;

        //すでにダメージ表示が存在する場合、新しいTextを作らず現在の数字へ加算する
        if (currentPopup != null)
        {
            //現在表示中のダメージへ追加
            accumulatedDamage += actualDamage;

            //表示されている数字を更新
            UpdateText(currentPopup, accumulatedDamage);

            //消滅までの時間をリセットするため、現在動いているCoroutineを停止
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            //新しく移動・消滅処理を開始
            currentCoroutine = StartCoroutine(MoveAndDestroy(currentPopup));

            return;
        }

        //初めてダメージを表示する場合
        accumulatedDamage = actualDamage;

        //ダメージTextをPrefabから生成
        //Canvasの子として生成することでUIとして表示する
        currentPopup = Instantiate(damageUI,popupCanvas.transform);

        //対象オブジェクトの左上へ配置
        SetPopupPosition(currentPopup);

        //ダメージ数値を設定
        UpdateText(currentPopup, accumulatedDamage);

        //上へ移動して消える処理を開始
        currentCoroutine = StartCoroutine(MoveAndDestroy(currentPopup));
    }

    //ワールド座標にある対象をCanvas上の座標へ変換する
    private void SetPopupPosition(GameObject popup)
    {
        //UI位置を変更するためRectTransformを取得
        RectTransform popupRect = popup.GetComponent<RectTransform>();

        //キャラクターのワールド座標を画面座標へ変換
        Vector2 screenPosition =　Camera.main.WorldToScreenPoint(targetObject.position);

        //CanvasのRectTransformを取得
        RectTransform canvasRect =　popupCanvas.GetComponent<RectTransform>();

        //スクリーン座標をCanvas内のローカル座標へ変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            popupCanvas.worldCamera,
            out Vector2 localPoint
        );

        //キャラクター位置から左上へずらす
        //数値を変更することで表示位置を調整可能
        localPoint += new Vector2(-150f, 200f);

        //Canvas内の表示位置を設定
        popupRect.localPosition = localPoint;
    }

    //ダメージTextの数字を更新する
    private void UpdateText(GameObject damagePopupObject, int amount)
    {
        //Textコンポーネントを取得
        Text legacyText = damagePopupObject.GetComponent<Text>();

        //Textが存在する場合のみ文字を変更
        if (legacyText != null)
        {
            legacyText.text = amount.ToString();
        }
    }

    //ダメージTextを上方向へ移動させ、時間経過で透明化して削除する処理
    IEnumerator MoveAndDestroy(GameObject damagePopupObject)
    {
        //経過時間
        float time = 0;

        //表示時間
        float lifeTime = 1.0f;

        //Textの色変更用
        Text legacyText =　damagePopupObject.GetComponent<Text>();

        // UI移動用
        RectTransform rect = damagePopupObject.GetComponent<RectTransform>();

        // 指定時間まで表示を継続
        while (time < lifeTime)
        {
            //時間を進める
            time += Time.deltaTime;

            //Textを上方向へ移動
            rect.localPosition += Vector3.up * (Time.deltaTime * 50f);

            //時間経過に応じて透明にする
            float alpha = 1f - (time / lifeTime);

            if (legacyText != null)
            {
                //現在の色を取得
                Color color = legacyText.color;

                //アルファ値だけ変更
                color.a = alpha;

                //Textへ反映
                legacyText.color = color;
            }

            //次のフレームまで待機
            yield return null;
        }

        //自分自身が現在表示中のTextなら参照を解除
        if (damagePopupObject == currentPopup)
        {
            currentPopup = null;
        }

        //表示終了後に削除
        Destroy(damagePopupObject);
    }
}