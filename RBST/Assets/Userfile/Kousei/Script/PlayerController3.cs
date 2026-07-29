namespace Game.Player
{
    using UnityEngine;

    public class PlayerController3 : MonoBehaviour
    {
        private const float SCALE_X = 0.15f;
        private const float SCALE_Y = 0.15f;
        private const float SCALE_Z = 0.15f;

        private static readonly int IS_MOVING_HASH
            = Animator.StringToHash("IsMoving");

        private static readonly int ATTACK_HASH
            = Animator.StringToHash("Attack");

        [SerializeField]
        private float speed_ = 5.0f;

        [SerializeField]
        private Animator animator_;

        private void Update()
        {
            Move();
            Attack();
        }

        private void Move()
        {
            float horizontalInput
                = Input.GetAxisRaw("Horizontal");

            float verticalInput
                = Input.GetAxisRaw("Vertical");

            Vector2 moveDirection
                = new Vector2(
                    horizontalInput,
                    verticalInput
                );

            bool isMoving
                = moveDirection != Vector2.zero;

            animator_.SetBool(
                IS_MOVING_HASH,
                isMoving
            );

            if (!isMoving)
            {
                return;
            }

            transform.position
                += (Vector3)moveDirection.normalized
                * speed_
                * Time.deltaTime;

            if (horizontalInput < 0.0f)
            {
                transform.localScale
                    = new Vector3(
                        SCALE_X,
                        SCALE_Y,
                        SCALE_Z
                    );
            }
            else if (horizontalInput > 0.0f)
            {
                transform.localScale
                    = new Vector3(
                        -SCALE_X,
                        SCALE_Y,
                        SCALE_Z
                    );
            }
        }

        private void Attack()
        {
            bool isAttackInput
                = Input.GetMouseButtonDown(0)
                || Input.GetMouseButtonDown(1)
                || Input.GetMouseButtonDown(2);

            if (!isAttackInput)
            {
                return;
            }

            animator_.SetTrigger(ATTACK_HASH);
        }
    }
}