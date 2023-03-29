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

        [SerializeField]
        private SpriteRenderer renderer;

        [SerializeField] private float startViewDirection;

        private void Start()
        {
            desiredWalkDirection = new Vector2(Mathf.Clamp(startViewDirection, -1f, 1f), 0f);
        }

        private void OnDisable()
        {
            GetComponent<EnemyMovement>().enabled = false;
        }

        private void SetDirection(bool right)
        {
            renderer.flipX = !right;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, desiredWalkDirection);
        }
    }
}
