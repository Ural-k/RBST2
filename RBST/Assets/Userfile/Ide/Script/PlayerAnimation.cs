using UnityEngine;

public class PlayerAnimation : MonoBehaviour

{

    //スプライトを表示するためのRenderer

    [SerializeField] private SpriteRenderer spriteRenderer;

    //待機時に表示するスプライト

    [SerializeField] private Sprite idleSprite;

    //移動中に表示するスプライト

    [SerializeField] private Sprite moveSprite;

    //投げアニメーション1枚目

    [SerializeField] private Sprite throw1;

    //投げアニメーション2枚目

    [SerializeField] private Sprite throw2;

    //投げアニメーション1枚あたりの表示時間

    private float throwAnimationTime = 0.15f;

    //投げアニメーション経過時間を管理するタイマー

    private float timer;

    //現在投げアニメーション中かどうか

    //投げ中は歩きアニメーションなどを上書きしない

    private bool throwing;

    void Update()

    {

        //投げアニメーション中の場合のみ更新する

        if (throwing)

        {

            ThrowAnimation();

        }

    }

    //歩き・待機アニメーションを切り替える処理

    //movingがtrueなら歩き、falseなら待機

    public void SetWalking(bool moving)

    {

        //投げアニメーション中は歩きアニメーションを変更しない

        //投げモーションを優先するため

        if (throwing)

        {

            return;

        }

        //移動中の場合

        if (moving)

        {

            //歩き用スプライトへ変更

            spriteRenderer.sprite = moveSprite;

        }

        else

        {

            //待機用スプライトへ変更

            spriteRenderer.sprite = idleSprite;

        }

    }

    //投げアニメーション開始処理

    //PlayerControllerから呼び出される

    public void StartThrow()

    {

        //投げアニメーション開始

        throwing = true;

        //タイマーをリセット

        timer = 0;

    }

    //投げアニメーションの更新処理

    void ThrowAnimation()

    {

        //経過時間を加算

        timer += Time.deltaTime;

        //1枚目の投げスプライトを表示

        if (timer < throwAnimationTime)

        {

            spriteRenderer.sprite = throw1;

        }

        //2枚目の投げスプライトを表示

        else if (timer < throwAnimationTime * 2)

        {

            spriteRenderer.sprite = throw2;

        }

        //アニメーション終了

        else

        {

            //投げ状態を解除

            throwing = false;

            //タイマーをリセット

            timer = 0;

            //待機状態のスプライトへ戻す

            spriteRenderer.sprite = idleSprite;

        }

    }

}
