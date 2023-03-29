using UnityEngine;

namespace Game
{
    public class RainZone : MonoBehaviour
    {
        private PlayerController2D player;
        private Pickable parasolPickable;
        private Parasol parasol;

        private void Update()
        {
            if (!player)
                return;
            if (CheckParasol())
            {
                parasolPickable.StartInteraction();
                parasol.enabled = false;
            }
        }

        private bool CheckParasol()
        {
            // Still holding parasol
            if (parasol && player.pickHandler.pickedObject && parasol.gameObject && player.pickHandler.pickedObject.gameObject)
                return true;
            // We found parasol
            if (player.pickHandler.picked && player.pickHandler.pickedObject.TryGetComponent(out parasol))
            {
                parasolPickable = player.pickHandler.pickedObject;
                return true;
            }
            // Player doesn't hold parasol
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
            if (parasolPickable)
                parasolPickable.StopInteraction();
            player = null;
            parasolPickable = null;
            parasol = null;
        }
    }
}
