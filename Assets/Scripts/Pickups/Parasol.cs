using UnityEngine;

namespace Game
{
    public class Parasol : MonoBehaviour
    {
        [SerializeField] private float maxFallVelocity;

        private Rigidbody2D target;

        private void OnEnable()
        {
            target = GetComponentInParent<PlayerController2D>().rb;
        }

        private void OnDisable()
        {
            target = null;
        }

        private void FixedUpdate()
        {
            var velocity = target.velocity;
            if (velocity.y < 0f)
                velocity.y = Mathf.Max(velocity.y, -maxFallVelocity);
            target.velocity = velocity;
        }
    }
}
