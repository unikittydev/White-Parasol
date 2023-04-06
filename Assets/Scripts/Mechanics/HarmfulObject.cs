using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HarmfulObject : MonoBehaviour
    {
        [SerializeField] private int damage;

        private List<Damageable> targets = new();

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            for (int i = 0; i < targets.Count; i++)
                targets[i].TakeDamage(damage);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out Damageable damageable))
            {
                if (!enabled)
                    enabled = true;
                targets.Add(damageable);
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out Damageable damageable))
            {
                targets.Remove(damageable);
                if (targets.Count == 0)
                    enabled = false;
            }
        }
    }
}
