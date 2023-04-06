using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

namespace Game
{
    public class BossStateTimer : MonoBehaviour
    {
        [SerializeField] private float stateMinTime;
        [SerializeField] private float stateMaxTime;

        [SerializeField] private UnityEvent onStateCompletion;
        
        private void OnEnable()
        {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(Random.Range(stateMinTime, stateMaxTime));
            onStateCompletion?.Invoke();
        }
    }
}
