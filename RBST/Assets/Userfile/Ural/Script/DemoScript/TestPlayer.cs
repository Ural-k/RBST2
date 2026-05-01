using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayer : MonoBehaviour, IDamageable
{
    private Vector2 pos_;
    private float speed_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed_ = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //適当移動( ´艸｀)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        pos_.x += inputX * speed_ * Time.deltaTime;
        pos_.y += inputY * speed_ * Time.deltaTime;
        transform.position = pos_;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("当たっちゃった…ワイプワイプ");
    }
}
