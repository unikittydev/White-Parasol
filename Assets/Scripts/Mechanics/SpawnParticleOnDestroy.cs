using UnityEngine;

namespace Game
{
    public class SpawnParticleOnDestroy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private float lifetime;
        [SerializeField] private Color startColor;
        
        public void SpawnParticles()
        {
            var instance = Instantiate(ps, transform.position, Quaternion.identity, transform.parent);
            var main = instance.main;
            main.startColor = startColor;
            Destroy(instance.gameObject, lifetime);
        }
    }
}
