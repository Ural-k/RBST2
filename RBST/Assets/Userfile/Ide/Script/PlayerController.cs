using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;

    Animator animator;

    Vector2 movement;

    public Transform throwPoint;

    public GameObject bottlePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator =
        GetComponent<Animator>();
    }

    void Update()
    {
        // W A S D
        movement.x =
        Input.GetAxisRaw("Horizontal");

        movement.y =
        Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetBool(
            "isWalking",
            true);
        }
        else
        {
            animator.SetBool(
            "isWalking",
            false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ThrowBottle();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(
        rb.position +
        movement.normalized *
        speed *
        Time.fixedDeltaTime);
    }

    void ThrowBottle()
    {
        animator.SetTrigger("Throw");

        GameObject bottle =
        Instantiate(
        bottlePrefab,
        throwPoint.position,
        Quaternion.identity);

        Rigidbody2D bottleRb =
        bottle.GetComponent<Rigidbody2D>();

        bottleRb.linearVelocity =
        transform.right * 8f;
    }
}