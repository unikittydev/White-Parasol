using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(EnemyController2D))]
    public class EnemyWallAvoidBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask avoidLayer;
        [SerializeField] private float avoidRadius;
        [SerializeField] private Transform rayStartPivot;

        private EnemyController2D controller;

        private ContactFilter2D filter;

        private List<RaycastHit2D> results = new();
        
        private void Awake()
        {
            controller = GetComponent<EnemyController2D>();
            filter = new ContactFilter2D() { layerMask = avoidLayer, useTriggers = false };
        }

        private void FixedUpdate()
        {
            if (Physics2D.Raycast(rayStartPivot.position, controller.desiredWalkDirection, filter, results, avoidRadius) > 1)
            {
                controller.desiredWalkDirection = new Vector2(-controller.desiredWalkDirection.x, 0f);
            }    
        }
    }
}