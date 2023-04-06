using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class AudioTheme : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onTriggerEnter;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController2D _))
                onTriggerEnter?.Invoke();
        }
    }
}
