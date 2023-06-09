using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [Flags]
    public enum DamageSide
    {
        Top = 1,
        Sides = 2,
    }

    public class Damageable : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        
        [SerializeField] private LayerMask damageMask;
        
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private int _currentHealth = 1;
        [SerializeField] private float invinsibleTime = 2f;

        public int maxHealth => _maxHealth;
        public int currentHealth => _currentHealth;
        
        [SerializeField] private float deathJumpHeight = 5f;
        //[SerializeField] private bool destroyOnHealthZero;
        
        private float invinsibleCounter;
        private bool invinsible;
        
        [SerializeField] private DamageSide damageSides = DamageSide.Top | DamageSide.Sides;

        [SerializeField] private UnityEvent onTakeDamage;
        [SerializeField] private UnityEvent onHealthZero;

        public UnityEvent OnHealthZero => onHealthZero;
        
        public bool CanTakeDamage(DamageSide hitSide) => damageSides.HasFlag(hitSide);

        private void Update()
        {
            if (!invinsible)
                return;

            invinsibleCounter -= Time.deltaTime;
            if (invinsibleCounter <= 0f)
            {
                invinsible = false;
                rb.excludeLayers = 0;
            }
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            if (invinsible)
                return;
            
            _currentHealth -= damage;

            invinsible = true;
            rb.excludeLayers = damageMask;
            invinsibleCounter = invinsibleTime;
            
            onTakeDamage?.Invoke();
            
            if (currentHealth <= 0)
                onHealthZero?.Invoke();
        }

        public void PlayDeathAnimation()
        {
            rb.velocity = new Vector2(0f, deathJumpHeight);
        }

        public void Despawn()
        {
            SpawnSystem.Despawn(gameObject);
        }
    }
}
