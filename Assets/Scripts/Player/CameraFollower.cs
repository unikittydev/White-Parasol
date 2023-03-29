using UnityEngine;

namespace Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            if (!target)
                return;
            Vector3 camPos = transform.position;
            camPos.x = target.position.x;
            camPos.y = target.position.y;
            transform.position = camPos;
        }
    }
}