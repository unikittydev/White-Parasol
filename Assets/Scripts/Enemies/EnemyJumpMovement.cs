using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(EnemyController2D), typeof(Rigidbody2D), typeof(GroundChecker2D))]
    public class EnemyJumpMovement : EnemyMovement
    {
        private static int GROUNDED = Animator.StringToHash("GROUNDED");
        
        private EnemyController2D controller;
        private Rigidbody2D rb;
        private GroundChecker2D gc;
        private Animator animator;
        
        [SerializeField] private Vector2 jumpVelocity;
        [SerializeField] private float jumpReloadTime;

        private float jumpCounter;

        private void Awake()
        {
            controller = GetComponent<EnemyController2D>();
            rb = GetComponent<Rigidbody2D>();
            gc = GetComponent<GroundChecker2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetBool(GROUNDED, gc.isGrounded);
            
            if (!gc.isGrounded)
                return;
            
            jumpCounter += Time.deltaTime;

            if (jumpCounter < jumpReloadTime)
                return;
            
            Jump();
        }

        private void Jump()
        {
            Vector2 velocity = jumpVelocity;
            velocity.x *= controller.desiredWalkDirection.x > 0f ? 1f : -1f;
            rb.velocity = velocity;
            jumpCounter = 0f;
        }
    }
}