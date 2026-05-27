using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform cube_;
    [SerializeField] public float speed_;
    private Rigidbody2D playerRigidbody_;

    private void Start()
    {
        playerRigidbody_ = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        cube_.Translate(move * speed_ * Time.deltaTime);
    }
}