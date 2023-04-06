using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyWallAvoidBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask avoidLayer;
        [SerializeField] private float avoidRadius;
        [SerializeField] private Transform rayStartPivot;
        
        [SerializeField]
        private EnemyController2D controller;

        private ContactFilter2D filter;

        private List<RaycastHit2D> results = new();
        
        private void OnEnable()
        {
            filter = new ContactFilter2D() { useLayerMask = true, layerMask = avoidLayer, useTriggers = false };
        }

        private void FixedUpdate()
        {
            if (Physics2D.Raycast(rayStartPivot.position, controller.lookDirection, filter, results, avoidRadius) > 0)
            {
                bool foundSolidObstacle = false;
                foreach (var hit in results)
                {
                    if (hit.collider.gameObject == controller.gameObject)
                        continue;
                    // Not the best but.
                    if (hit.collider.TryGetComponent(out PlatformEffector2D _))
                        continue;
                    foundSolidObstacle = true;
                    break;
                }
                
                if (foundSolidObstacle)
                    controller.desiredWalkDirection = new Vector2(-controller.desiredWalkDirection.x, 0f);
            }    
        }
    }
}