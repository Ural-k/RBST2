using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    //生成するダメージ表示用TextのPrefab
    [SerializeField] private GameObject damageUI_;

    //ダメージ表示を配置するCanvas
    [SerializeField] private Canvas popupCanvas_;

    //ダメージ表示を追従させる対象(敵やプレイヤー)
    [SerializeField] private Transform targetObject_;

    //テスト時に使用する初期ダメージ量
    [SerializeField] private int defaultDamage_;

    //現在表示中のダメージText
    //nullの場合は現在表示されていない
    private GameObject currentPopup_;

    //現在表示中の合計ダメージ量
    //短時間に複数回攻撃を受けた場合、この値へ加算する
    private int accumulatedDamage_ = 0;

    //ダメージTextの移動・消滅処理を管理するCoroutine
    private Coroutine currentCoroutine_;

    private Rigidbody2D playerRigidbody_;
    public Transform cube_;
    public float speed_;

    private void Start()
    {
        playerRigidbody_ = GetComponent<Rigidbody2D>();
    }

    //動作確認用
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        cube_.Translate(move * speed_ * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(defaultDamage_);
        }
    }

    //ダメージを受けた時に呼び出す処理
    public void TakeDamage(int damage = -1)
    {
        //引数が指定されていない場合は設定した初期ダメージを使用
        int actualDamage = (damage < 0) ? defaultDamage_ : damage;

        //すでにダメージ表示が存在する場合、新しいTextを作らず現在の数字へ加算する
        if (currentPopup_ != null)
        {
            //現在表示中のダメージへ追加
            accumulatedDamage_ += actualDamage;

            //表示されている数字を更新
            UpdateText(currentPopup_, accumulatedDamage_);

            //消滅までの時間をリセットするため、現在動いているCoroutineを停止
            if (currentCoroutine_ != null)
            {
                StopCoroutine(currentCoroutine_);
            }

            //新しく移動・消滅処理を開始
            currentCoroutine_ = StartCoroutine(MoveAndDestroy(currentPopup_));

            return;
        }

        //初めてダメージを表示する場合
        accumulatedDamage_ = actualDamage;

        //ダメージTextをPrefabから生成
        //Canvasの子として生成することでUIとして表示する
        currentPopup_ = Instantiate(damageUI_,popupCanvas_.transform);

        //対象オブジェクトの左上へ配置
        SetPopupPosition(currentPopup_);

        //ダメージ数値を設定
        UpdateText(currentPopup_, accumulatedDamage_);

        //上へ移動して消える処理を開始
        currentCoroutine_ = StartCoroutine(MoveAndDestroy(currentPopup_));
    }

    //ワールド座標にある対象をCanvas上の座標へ変換する
    private void SetPopupPosition(GameObject popup)
    {
        //UI位置を変更するためRectTransformを取得
        RectTransform popupRect = popup.GetComponent<RectTransform>();

        //キャラクターのワールド座標を画面座標へ変換
        Vector2 screenPosition =　Camera.main.WorldToScreenPoint(targetObject_.position);

        //CanvasのRectTransformを取得
        RectTransform canvasRect =　popupCanvas_.GetComponent<RectTransform>();

        //スクリーン座標をCanvas内のローカル座標へ変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            popupCanvas_.worldCamera,
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
        float lifeTime = 0.5f;

        //Textの色変更用
        Text legacyText =　damagePopupObject.GetComponent<Text>();

        //UI移動用
        RectTransform rect = damagePopupObject.GetComponent<RectTransform>();

        //指定時間まで表示を継続
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
        if (damagePopupObject == currentPopup_)
        {
            currentPopup_ = null;
        }

        //表示終了後に削除
        Destroy(damagePopupObject);
    }
}