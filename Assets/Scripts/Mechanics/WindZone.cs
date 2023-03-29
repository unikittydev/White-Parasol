using UnityEngine;

namespace Game
{
    public class WindZone : MonoBehaviour
    {
        [SerializeField] private float windPower;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool overrideGravity;
        
        private PlayerController2D player;
        private Pickable parasol;

        private void Awake()
        {
            var ps = GetComponent<ParticleSystem>();
            ParticleSystem.VelocityOverLifetimeModule vol = ps.velocityOverLifetime;
            vol.enabled = true;
            vol.speedModifierMultiplier = windPower;
            vol.x = new ParticleSystem.MinMaxCurve(direction.x);
            vol.y = new ParticleSystem.MinMaxCurve(direction.y);
        }

        private void FixedUpdate()
        {
            if (!player)
                return;
            if (!CheckForParasol())
                return;
            
            var velocity = direction * windPower;
            if (velocity.x != 0f)
                player.OverrideVelocityX(velocity.x);
            if (velocity.y != 0f)
                player.OverrideVelocityY(velocity.y);
            else if (overrideGravity)
                player.OverrideVelocityY(0f);
        }

        private bool CheckForParasol()
        {
            if (parasol && parasol.activated)
                return true;

            if (!parasol && player.pickHandler.pickedObject && player.pickHandler.pickedObject.GetComponent<Parasol>() != null)
                parasol = player.pickHandler.pickedObject;
            
            return false;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out PlayerController2D _player))
                return;
            player = _player;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!player || player.gameObject != other.gameObject)
                return;
            player = null;
            parasol = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, .5f);
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
