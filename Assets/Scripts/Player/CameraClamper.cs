using UnityEngine;

namespace Game
{
    public class CameraClamper : MonoBehaviour
    {
        [SerializeField]
        private CameraFollower follower;

        [SerializeField] private SetCameraBoundsTag bounds;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out SetCameraBoundsTag bounds))
            {
                this.bounds = bounds;
                follower.SetClampBoundaries(bounds.minPosition, bounds.maxPosition);
            }
            else if (col.TryGetComponent(out ResetCameraBoundsTag _))
                follower.ResetBoundaries();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out SetCameraBoundsTag bounds) && this.bounds == bounds)
            {
                this.bounds = null;
                follower.ResetBoundaries();
            }
        }
    }
}