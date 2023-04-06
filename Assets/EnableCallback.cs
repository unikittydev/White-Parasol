using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnableCallback : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnable;

        private void OnEnable()
        {
            onEnable?.Invoke();
        }
    }
}
