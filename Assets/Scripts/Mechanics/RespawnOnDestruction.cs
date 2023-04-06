using UnityEngine;

namespace Game
{
    public class RespawnOnDestruction : MonoBehaviour
    {
        private GameObject clone;

        [SerializeField] private bool instantiateClone = true;
        
        private void Start()
        {
            if (!instantiateClone)
                return;
            clone = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            clone.SetActive(false);
        }

        public void Respawn()
        {
            Destroy(gameObject);
            if (instantiateClone)
                clone.SetActive(true);
        }
    }
}
