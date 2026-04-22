using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayer : MonoBehaviour
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
        //ìKìñà⁄ìÆ( ÅL‰áÅM)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        pos_.x += inputX * speed_ * Time.deltaTime;
        pos_.y += inputY * speed_ * Time.deltaTime;
        transform.position = pos_;
    }
}
