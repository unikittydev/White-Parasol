using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SetCameraBoundsTag : MonoBehaviour
    {
        [SerializeField] private Transform minPivot;
        [SerializeField] private Transform maxPivot;

        public Vector3 minPosition => minPivot.position;
        public Vector3 maxPosition => maxPivot.position;
        
        [ExecuteAlways]
        private void OnDrawGizmosSelected()
        {
            if (!minPivot || !maxPivot)
                return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube((minPivot.position + maxPivot.position) * .5f, maxPivot.position - minPivot.position);
        }
    }
}