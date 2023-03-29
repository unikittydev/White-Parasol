using UnityEngine;

namespace Game
{
    public class EnemyFlyMovement : EnemyMovement
    {
        private Rigidbody2D rb;
        private EnemyController2D controller;
        
        [SerializeField] private float horizontalMovement;
        [SerializeField] private float swingAmplitude;
        [SerializeField] private float swingSpeed;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            controller = GetComponent<EnemyController2D>();
        }

        private void FixedUpdate()
        {
            Vector2 velocity = new Vector2(horizontalMovement, Mathf.Cos(Time.time * swingSpeed) * swingAmplitude);
            velocity.x *= controller.desiredWalkDirection.x > 0f ? 1f : -1f;
            rb.velocity = velocity;
        }
    }
}
