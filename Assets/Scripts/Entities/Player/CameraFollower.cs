using UnityEngine;

namespace Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 cameraOffset;

        [SerializeField] private Vector3 clampMin;
        [SerializeField] private Vector3 clampMax;
        [SerializeField] private bool clamp;

        private Camera camera;
        private Vector2 size;

        private void Awake()
        {
            camera = GetComponent<Camera>();
            size.y = camera.orthographicSize * 2f;
            size.x = size.y * Screen.width / Screen.height;
        }
        
        private void LateUpdate()
        {
            Vector3 targetPosition = target.position + cameraOffset;
            if (clamp)
                targetPosition = ClampCameraPosition(targetPosition);

            transform.position = targetPosition;
        }

        private Vector3 ClampCameraPosition(Vector3 targetPosition)
        {
            return new Vector3(
                Mathf.Clamp(targetPosition.x, clampMin.x + size.x * .5f, clampMax.x - size.x * .5f),
                Mathf.Clamp(targetPosition.y, clampMin.y + size.y * .5f, clampMax.y - size.y * .5f),
                targetPosition.z);
        }

        public void SetClampBoundaries(Vector3 min, Vector3 max)
        {
            clamp = true;
            clampMin = min;
            clampMax = max;
        }

        public void ResetBoundaries()
        {
            clamp = false;
        }
    }
}