using UnityEngine;

namespace Game
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject boss;

        private GameObject bossInstance;

        private void Start()
        {
            boss.SetActive(false);
        }

        private void SpawnBossClone()
        {
            bossInstance = Instantiate(boss, parent);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController2D player))
            {
                SpawnBossClone();
                bossInstance.gameObject.SetActive(true);
            }
        }

        public void DespawnBoss()
        {
            Destroy(bossInstance);
        }
    }
}
