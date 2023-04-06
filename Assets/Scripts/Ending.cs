using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Ending : MonoBehaviour
    {
        [SerializeField] private PlayerController2D player;
        [SerializeField] private CameraFollower camera;
        
        [SerializeField] private int totalGemsCount = 4;
        [SerializeField] private float goodEndingTintDelay;

        [SerializeField] private UnityEvent onGoodEnding;
        [SerializeField] private UnityEvent onGoodEndingAfterDelay;
        [SerializeField] private UnityEvent onNormalEnding;
        
        private int gemsCollected;
        
        public void CollectGem()
        {
            gemsCollected++;
        }

        public void ActivateEnding()
        {
            transform.parent = player.transform;
            transform.localPosition = Vector3.zero;

            if (gemsCollected == totalGemsCount)
            {
                onGoodEnding?.Invoke();
                StartCoroutine(Delay());
            }
            else
                onNormalEnding?.Invoke();
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(goodEndingTintDelay);
            onGoodEndingAfterDelay?.Invoke();
        }
    }
}
