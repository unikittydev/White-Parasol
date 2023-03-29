using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GroundChecker2D : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckPivot;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = .2f;

        private ContactFilter2D filter;
        private List<Collider2D> results = new();
        
        public bool isGrounded { get; private set; }

        private void Start()
        {
            filter = new ContactFilter2D { layerMask = groundLayer, useTriggers = false };
        }

        private void FixedUpdate()
        {
            CheckGrounded();
        }
        
        private void CheckGrounded()
        {
            int contacts = Physics2D.OverlapCircle(groundCheckPivot.position, groundCheckRadius, filter, results);
            isGrounded = contacts > 1;
        }
    }
}