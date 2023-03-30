using System.Collections;
using UnityEngine;

namespace Game
{
    public class Gem : MonoBehaviour
    {
        [SerializeField] private float collectionTime;

        public void Collect()
        {
            StartCoroutine(WaitForCollection());
        }

        private IEnumerator WaitForCollection()
        {
            yield return new WaitForSeconds(collectionTime);
            GetComponent<SpawnParticleOnDestroy>().SpawnParticles();
            Destroy(gameObject);
        }
    }
}
