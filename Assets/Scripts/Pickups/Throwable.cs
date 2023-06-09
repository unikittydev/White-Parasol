using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Throwable : MonoBehaviour
    {
        [SerializeField] private float throwVelocity;
        [SerializeField] private float invinsibleTimer = .05f;

        [SerializeField] private UnityEvent _onHit;
        public UnityEvent onHit => _onHit;
        
        [SerializeField] private bool throwOnStart;
        
        private PickHandler handler;
        
        private bool invinsible;
        
        private float viewDirection;

        private void Start()
        {
            if (throwOnStart)
                Throw();
        }

        public void SetThrowDirection()
        {
            handler = GetComponentInParent<PickHandler>();
            viewDirection = transform.lossyScale.x;
        }
        
        public void Throw()
        {
            StartCoroutine(CountInvinsibility());
            var rb = GetComponent<Rigidbody2D>();
            Vector2 velocity = rb.velocity;
            velocity.x = throwVelocity * viewDirection;
            rb.velocity = velocity;
        }

        private IEnumerator CountInvinsibility()
        {
            invinsible = true;
            yield return new WaitForSeconds(invinsibleTimer);
            invinsible = false;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!throwOnStart && !handler)
                return;
            if (handler && col.gameObject == handler.gameObject)
                return;
            // Ignore self hit
            if (col.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(1);
                Destroy(gameObject);
                handler = null;
                onHit?.Invoke();
            }
            else if (TryGetComponent(out Damageable selfDamageable))
            {
                handler = null;
                onHit?.Invoke();
            }
        }
    }
}
