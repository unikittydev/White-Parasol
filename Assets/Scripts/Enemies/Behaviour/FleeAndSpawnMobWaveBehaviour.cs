using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game
{
    public class FleeAndSpawnMobWaveBehaviour : MonoBehaviour
    {
        [Header("Movement settings")]
        [SerializeField] private EnemyController2D controller;
        [SerializeField] private Transform fleePoint;
        [SerializeField] private Transform returnPoint;
        [SerializeField] private float pointRadius;
        
        [Header("Enemy settings")]
        [SerializeField] private Transform enemyParent;
        [SerializeField] private BoxCollider2D enemySpawnVolume;
        [SerializeField] private Damageable[] enemyPrefabs;
        [SerializeField] private int enemyCount;
        [SerializeField] private float spawnReloadTime;
        [SerializeField] private bool forceReturnAfterSpawn;

        private int enemyRemaining;
        
        [SerializeField] private UnityEvent onAllMobsDied;
        
        private void OnEnable()
        {
            StartCoroutine(FleeAndSpawn());
        }

        private IEnumerator FleeAndSpawn()
        {
            enemyRemaining = enemyCount;

            controller.desiredWalkDirection = (fleePoint.position - transform.position).normalized;
            // Flee
            yield return new WaitUntil(() =>
                (transform.position - fleePoint.position).sqrMagnitude < pointRadius * pointRadius);
            
            controller.desiredWalkDirection = Vector2.zero;
            
            // Spawn
            for (int i = 0; i < enemyCount; i++)
            {
                var pos = GetEnemyPosition();
                var enemy = Instantiate(GetRandomPrefab(), pos, Quaternion.identity, enemyParent);
                enemy.OnHealthZero.AddListener(() => enemyRemaining--);
                yield return new WaitForSeconds(spawnReloadTime);
            }
            
            // Wait
            if (!forceReturnAfterSpawn)
                yield return new WaitUntil(() => enemyRemaining == 0);
            
            controller.desiredWalkDirection = (returnPoint.position - transform.position).normalized;
            // Return
            yield return new WaitUntil(() =>
                (transform.position - returnPoint.position).sqrMagnitude < pointRadius * pointRadius);
            onAllMobsDied?.Invoke();
        }

        private Vector2 GetEnemyPosition()
        {
            var bounds = enemySpawnVolume.bounds;
            return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
        }

        private Damageable GetRandomPrefab() => enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }
}
