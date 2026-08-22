using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //スプライトを表示するためのRenderer
    [SerializeField] private SpriteRenderer spriteRenderer_;

    //待機時に表示するスプライト
    [SerializeField] private Sprite idleSprite_;

    //移動中に表示するスプライト
    [SerializeField] private Sprite moveSprite_;

    //投げアニメーション1枚目
    [SerializeField] private Sprite throw1_;

    //投げアニメーション2枚目
    [SerializeField] private Sprite throw2_;

    //投げアニメーション1枚あたりの表示時間
    private float throwAnimationTime_ = 0.15f;

    //投げアニメーション経過時間を管理するタイマー
    private float timer_;

    //現在投げアニメーション中かどうか
    //投げ中は歩きアニメーションなどを上書きしない
    private bool isThrowing_;

    void Update()
    {
        //投げアニメーション中の場合のみ更新する
        if (isThrowing_)
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
        if (isThrowing_)
        {
            return;
        }

        //移動中の場合
        if (moving)
        {
            //歩き用スプライトへ変更
            spriteRenderer_.sprite = moveSprite_;
        }
        else
        {
            //待機用スプライトへ変更
            spriteRenderer_.sprite = idleSprite_;
        }
    }

    //投げアニメーション開始処理
    //PlayerControllerから呼び出される
    public void StartThrow()
    {
        //投げアニメーション開始
        isThrowing_ = true;

        //タイマーをリセット
        timer_ = 0;
    }

    //投げアニメーションの更新処理
    void ThrowAnimation()
    {
        //経過時間を加算
        timer_ += Time.deltaTime;

        //1枚目の投げスプライトを表示
        if (timer_ < throwAnimationTime_)
        {
            spriteRenderer_.sprite = throw1_;
        }

        //2枚目の投げスプライトを表示
        else if (timer_ < throwAnimationTime_ * 2)
        {
            spriteRenderer_.sprite = throw2_;
        }

        //アニメーション終了
        else
        {
            //投げ状態を解除
            isThrowing_ = false;

            //タイマーをリセット
            timer_ = 0;

            //待機状態のスプライトへ戻す
            spriteRenderer_.sprite = idleSprite_;
        }
    }
}
