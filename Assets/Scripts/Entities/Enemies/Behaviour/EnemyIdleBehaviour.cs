using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyIdleBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyController2D controller;
        
        [SerializeField] private float idleTime;

        [SerializeField] private UnityEvent onIdleComplete;

        private void OnEnable()
        {
            StartCoroutine(Idle());
        }

        private IEnumerator Idle()
        {
            controller.desiredWalkDirection = Vector2.zero;
            yield return new WaitForSeconds(idleTime);
            onIdleComplete?.Invoke();
        }
    }
}