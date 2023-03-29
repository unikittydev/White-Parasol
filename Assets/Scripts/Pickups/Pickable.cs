using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum PickableSize
    {
        Small,
        Big
    }
    
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private Vector3 pickedOffset;
        
        [SerializeField] private UnityEvent onPickup;
        [SerializeField] private UnityEvent onDrop;
        [SerializeField] private UnityEvent onInteractionStart;
        [SerializeField] private UnityEvent onInteractionEnd;

        [field: SerializeField]
        public PickableSize size { get; private set; } = PickableSize.Small;
        
        private Rigidbody2D rb;
        
        private Transform target;

        private Transform defaultParent;
        private Quaternion defaultRotation;

        public bool activated { get; set; }
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!target)
                return;
            transform.position = target.position + pickedOffset;
            transform.rotation = target.rotation;
        }

        public void Pick(Transform target, Transform parent)
        {
            rb.simulated = false;
            this.target = target;
            defaultParent = transform.parent;
            transform.parent = parent;
            transform.localScale = Vector3.one;
            
            onDrop.AddListener(target.GetComponentInParent<PickHandler>().ForceDropObject);
            
            onPickup?.Invoke();
        }

        public void Drop()
        {
            var handler = target.GetComponentInParent<PickHandler>();
            
            activated = false;
            rb.simulated = true;
            target = null;
            transform.parent = defaultParent;

            onDrop?.Invoke();
            onDrop.RemoveListener(handler.ForceDropObject);
        }

        public void StartInteraction()
        {
            activated = true;
            onInteractionStart?.Invoke();
        }

        public void StopInteraction()
        {
            activated = false;
            onInteractionEnd?.Invoke();
        }
    }
}
