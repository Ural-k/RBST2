using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Text text_;            //対象のtext
    [SerializeField] private float lifeTime_ = 0.8f;  //表示される時間
    [SerializeField] private Vector3 moveOffset_ = new Vector3(0, 60f, 0);  //移動する距離のオフセット

    private float timer_;  //時間のカウント
    private Vector3 startPos_;  //起動する位置
    private Vector3 endPos_;  //終了する位置

    public void Setup(Vector3 worldPos, int value, FloatingTextType type)
    {
        text_.text = value.ToString();

        //敵、味方、回復かを判定
        switch (type)
        {
            case FloatingTextType.EnemyDamage:
                text_.color = Color.white;
                break;
            case FloatingTextType.PlayerDamage:
                text_.color = Color.red;
                break;
            case FloatingTextType.Heal:
                text_.color = Color.green;
                text_.text = "+" + value;
                break;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;

        startPos_ = transform.position;
        endPos_ = startPos_ + moveOffset_;
        timer_ = 0f;
    }

    private void Update()
    {
        timer_ += Time.deltaTime;
        float t = timer_ / lifeTime_;

        transform.position = Vector3.Lerp(startPos_, endPos_, t);

        if (timer_ >= lifeTime_)
        {
            Destroy(gameObject);
        }
    }
}