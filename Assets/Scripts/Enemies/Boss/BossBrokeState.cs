using UnityEngine;

namespace Game
{
    public class BossBrokeState : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;
        [SerializeField] private EnemyController2D controller;

        [SerializeField] private LayerMask newExcludeLayers;
        
        private LayerMask excludeLayers;
        
        private void OnEnable()
        {
            controller.enabled = false;
            rb.gravityScale = 1f;
            excludeLayers = col.forceReceiveLayers;
            col.forceReceiveLayers = newExcludeLayers;
        }

        private void OnDisable()
        {
            controller.enabled = true;
            rb.gravityScale = 0f;
            col.forceReceiveLayers = excludeLayers;
        }
    }
}
