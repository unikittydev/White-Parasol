using System.Collections;
using UnityEngine;

namespace Game
{
    public class SpawnSystem : MonoBehaviour
    {
        private static SpawnSystem instance;
        
        [SerializeField] private PlayerController2D player;

        [SerializeField] private Transform defaultRespawnPoint;

        [SerializeField] private float respawnTimer;
        [SerializeField] private float despawnTimer;
        
        private Checkpoint currentCheckpoint;

        public static Checkpoint current => instance.currentCheckpoint;
        
        private void Awake()
        {
            instance = this;
        }

        public static void SetActiveCheckpoint(Checkpoint checkpoint)
        {
            if (instance.currentCheckpoint)
                instance.currentCheckpoint.Deactivate();
            instance.currentCheckpoint = checkpoint;
        }

        public static void RespawnPlayer()
        {
            instance.StartCoroutine(instance.Respawn());
        }

        public static void Despawn(GameObject go)
        {
            Destroy(go, instance.despawnTimer);
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(respawnTimer);

            Camera.main.GetComponent<CameraFollower>().enabled = true;
            player.enabled = true;
            player.rb.velocity = Vector2.zero;
            player.GetComponent<Damageable>().ResetHealth();
            player.GetComponent<Collider2D>().enabled = true;
            Transform respawnPoint = current ? current.transform : defaultRespawnPoint;
            player.transform.position = respawnPoint.position;
        }
    }
}
