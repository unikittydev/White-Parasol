using System.Collections;
using UnityEngine;

namespace Game
{
    public class DelayedSilencer : MonoBehaviour
    {
        [SerializeField] private AudioListener listener;
        [SerializeField] private float delay;

        private IEnumerator Start()
        {
            listener.enabled = false;
            yield return new WaitForSeconds(delay);
            listener.enabled = true;
        }
    }
}
