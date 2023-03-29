using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemyPatrolBehaviour : MonoBehaviour
    {
        private EnemyController2D controller;
        private EnemyMovement movement;
        
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float waypointRadius = .5f;
        [SerializeField] private float idleTime;

        [SerializeField] private bool chaceAfterPlayer;
        
        private int currentWaypointIndex;

        private bool idling;
        
        private void Awake()
        {
            controller = GetComponent<EnemyController2D>();
            movement = GetComponent<EnemyMovement>();
        }

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

            float destination = Mathf.Clamp(waypoint.position.x - transform.position.x, -1f, 1f);

            controller.desiredWalkDirection = new Vector2(destination, 0f);
            
            if (Mathf.Abs(waypoint.position.x - transform.position.x) < waypointRadius)
                StartCoroutine(Idle());
        }

        private IEnumerator Idle()
        {
            idling = true;
            movement.enabled = false;
            yield return new WaitForSeconds(idleTime);
            GetNextWaypoint();
            movement.enabled = true;
            idling = false;
        }

        private void GetNextWaypoint()
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (chaceAfterPlayer && col.TryGetComponent(out PlayerController2D player))
            {
                
            }
        }
    }
}