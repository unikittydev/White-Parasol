using System;
using UnityEngine;

namespace Game
{
    public class EnemyChaseTargetMovement : EnemyMovement
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private EnemyController2D controller;

        [SerializeField] private float moveSpeed;

        private void OnDisable()
        {
            rb.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            Vector2 velocity = controller.desiredWalkDirection * moveSpeed;
            rb.velocity = velocity;
        }
    }
}