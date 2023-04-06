using UnityEngine;

namespace Game
{
    public class EnemyWalkMovement : EnemyMovement
    {
        private static int WALKING = Animator.StringToHash("WALKING");
        
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private EnemyController2D controller;
        [SerializeField]
        private Animator animator;

        [SerializeField] private float walkSpeed;
        
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
