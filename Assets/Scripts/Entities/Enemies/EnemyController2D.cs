using System;
using UnityEngine;

namespace Game
{
    public class EnemyController2D : MonoBehaviour
    {
        private Vector2 _desiredWalkDirection;
        public Vector2 desiredWalkDirection
        {
            get => _desiredWalkDirection;
            set
            {
                _desiredWalkDirection = value.normalized;
                SetDirection(_desiredWalkDirection.x > 0f);
            }
        }

        public Vector2 lookDirection { get; set; }
        
        [SerializeField]
        private SpriteRenderer renderer;

        [SerializeField] private float startViewDirection;

        private void Start()
        {
            desiredWalkDirection = new Vector2(Mathf.Clamp(startViewDirection, -1f, 1f), 0f);
        }

        private void OnDisable()
        {
            if (TryGetComponent(out EnemyMovement movement))
                movement.enabled = false;
        }

        private void SetDirection(bool right)
        {
            lookDirection = right ? Vector2.right : Vector2.left;
            renderer.flipX = !right;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, desiredWalkDirection);
        }
    }
}
