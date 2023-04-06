using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Damageable))]
    public class HitDamager2D : MonoBehaviour
    {
        private Damageable damageable;
        
        [SerializeField] private LayerMask damageMask;
        [SerializeField] private float minTopDotProduct = 1f / Mathf.Sqrt(2f);

        [SerializeField] private int failSelfDamage = 1;
        [SerializeField] private int hitDamage = 1;
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (damageMask != (damageMask | (1 << col.gameObject.layer)))
                return;

            Vector2 delta = col.otherRigidbody.position - col.rigidbody.position;
            delta.Normalize();
            DamageSide side = Vector2.Dot(delta, Vector2.up) > minTopDotProduct ? DamageSide.Top : DamageSide.Sides;

            if (!col.gameObject.TryGetComponent(out Damageable otherDamageable))
                return;
            
            if (otherDamageable.CanTakeDamage(side))
                otherDamageable.TakeDamage(hitDamage);
            else
                damageable.TakeDamage(failSelfDamage);
        }
    }
}