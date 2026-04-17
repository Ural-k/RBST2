using UnityEngine;

public class BulletControll : MonoBehaviour
{
    [SerializeField] private float speed_;

    private Rigidbody2D rb2d_;
    private Vector3 pos_;
    void Start()
    {
        rb2d_ = GetComponent<Rigidbody2D>();
        pos_ = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(pos_ * speed_ * Time.deltaTime); 
    }
}
