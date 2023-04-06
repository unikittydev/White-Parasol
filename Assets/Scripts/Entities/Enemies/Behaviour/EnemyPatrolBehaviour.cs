using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyPatrolBehaviour : MonoBehaviour
    {
        [SerializeField]
        private EnemyController2D controller;
        [SerializeField]
        private EnemyMovement movement;
        
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float waypointRadius = .5f;
        [SerializeField] private float idleTime;

        [SerializeField] private bool ignoreHeight;
        [SerializeField] private bool waypointsAreRandom;
        
        [SerializeField] private UnityEvent onWaypointReached;
        [SerializeField] private UnityEvent onPatrolComplete;

        private int waypointsPassed;
        private int currentWaypointIndex;

        private bool idling;

        private void Start()
        {
            if (waypoints.Length == 0)
                enabled = false;
            else
                currentWaypointIndex = 0;
        }

        private void Update()
        {
            if (idling)
                return;
            
            Transform waypoint = waypoints[currentWaypointIndex];

            if (ignoreHeight)
            {
                float destination = Mathf.Clamp(waypoint.position.x - transform.position.x, -1f, 1f);

                controller.desiredWalkDirection = new Vector2(destination, 0f);
                
                if (Mathf.Abs(waypoint.position.x - transform.position.x) < waypointRadius)
                    StartCoroutine(Idle());
            }
            else
            {
                controller.desiredWalkDirection = Vector2.ClampMagnitude(waypoint.position - transform.position, 1f);
                
                if ((waypoint.position - transform.position).sqrMagnitude < waypointRadius * waypointRadius)
                    StartCoroutine(Idle());
            }
        }

        private IEnumerator Idle()
        {
            idling = true;
            movement.enabled = false;
            onWaypointReached?.Invoke();
            yield return new WaitForSeconds(idleTime);
            GetNextWaypoint();
            movement.enabled = true;
            idling = false;
            
            if (waypointsPassed == waypoints.Length)
                onPatrolComplete?.Invoke();
        }

        private void GetNextWaypoint()
        {
            waypointsPassed++;
            if (waypointsAreRandom)
                currentWaypointIndex = Random.Range(0, waypoints.Length);
            else
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}