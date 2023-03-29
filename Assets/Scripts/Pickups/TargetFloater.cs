using UnityEngine;

namespace Game
{
    public class TargetFloater : MonoBehaviour
    {
        private float defaultHeight;
        
        [SerializeField] private float amplitude;
        [SerializeField] private float speed;

        [SerializeField] private Transform target;

        private void OnEnable()
        {
            defaultHeight = target.localPosition.y;
        }

        private void OnDisable()
        {
            Vector3 oldPos = target.localPosition;
            oldPos.y = defaultHeight;
            target.localPosition = oldPos;
        }

        private void Update()
        {
            Vector3 oldPos = target.localPosition;
            oldPos.y = Mathf.Cos(speed * Time.time) * amplitude;
            target.localPosition = oldPos;
        }
    }
}
