using UnityEngine;

namespace Game
{
    public class EnemyWalkMovement : EnemyMovement
    {
        private static int WALKING = Animator.StringToHash("WALKING");
        
        private Rigidbody2D rb;
        private EnemyController2D controller;
        private Animator animator;

        [SerializeField] private float walkSpeed;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            controller = GetComponent<EnemyController2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetBool(WALKING, controller.desiredWalkDirection.x != 0f);
        }

        private void FixedUpdate()
        {
            Vector2 velocity = new Vector2(walkSpeed, rb.velocity.y);
            velocity.x *= controller.desiredWalkDirection.x > 0f ? 1f : -1f;
            rb.velocity = velocity;
        }
    }
}
