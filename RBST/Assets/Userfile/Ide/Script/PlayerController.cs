using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject cube;
    [SerializeField] public float speed;
    private Rigidbody2D playerRigidbody;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
    }
}