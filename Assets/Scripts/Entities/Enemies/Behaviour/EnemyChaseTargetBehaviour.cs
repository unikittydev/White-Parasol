using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyChaseTargetBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyController2D controller;

        [SerializeField] private UnityEvent onDestinationReached;

        [SerializeField] private float reachDistance = .5f;

        [SerializeField]
        private Transform targetOnEnable;
        
        private Vector3 destination;

        public void SetTargetPosition(Vector3 value) => destination = value;

        public void SetTargetPosition(Transform target) => destination = target.position;

        private void OnEnable()
        {
            if (targetOnEnable)
                SetTargetPosition(targetOnEnable);
        }

        private void Update()
        {
            if ((destination - transform.position).sqrMagnitude < reachDistance * reachDistance)
            {
                onDestinationReached?.Invoke();
                return;
            }
            
            Vector3 delta = (destination - transform.position).normalized;
            controller.desiredWalkDirection = delta;
        }
    }
}
