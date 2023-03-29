using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Checkpoint : MonoBehaviour
    {
        private static int ACTIVE = Animator.StringToHash("ACTIVE");

        private Animator animator;
        
        private bool active;

        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private UnityEvent onDeactivate;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController2D _))
                Activate();
        }

        public void Activate()
        {
            if (active || SpawnSystem.current == this)
                return;
            
            SpawnSystem.SetActiveCheckpoint(this);
            animator.SetBool(ACTIVE, true);
            onActivate?.Invoke();
        }

        public void Deactivate()
        {
            animator.SetBool(ACTIVE, false);
            onDeactivate?.Invoke();
        }
    }
}
