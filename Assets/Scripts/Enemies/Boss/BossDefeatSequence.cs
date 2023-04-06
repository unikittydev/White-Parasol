using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BossDefeatSequence : MonoBehaviour
    {
        [SerializeField] private EnemyController2D controller;
        [Header("Fall")]
        [SerializeField] private Transform fallPoint;
        [SerializeField] private float fallSpeed;
        [SerializeField] private float fallRadius;

        [SerializeField] private UnityEvent onEnable;
        [SerializeField] private UnityEvent onAfterFall;
        
        private void OnEnable()
        {
            StartCoroutine(DefeatSequence());
        }

        private IEnumerator DefeatSequence()
        {
            controller.desiredWalkDirection = Vector2.zero;
            controller.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Transform target = controller.transform;
            onEnable?.Invoke();
            // Slowly Fall
            while ((target.position - fallPoint.position).sqrMagnitude > fallRadius * fallRadius)
            {
                Vector3 delta = (fallPoint.position - target.position).normalized * (fallSpeed * Time.deltaTime);
                target.position += delta;
                yield return null;
            }
            controller.GetComponent<Rigidbody2D>().gravityScale = 1f;
            onAfterFall?.Invoke();
        }
    }
}
