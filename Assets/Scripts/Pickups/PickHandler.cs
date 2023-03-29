using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PickHandler : MonoBehaviour
    {
        public Pickable pickedObject { get; private set; }
        private List<Collider2D> collidersInPickupZone = new();

        [SerializeField] private Collider2D pickupTrigger;
        [SerializeField] private Transform pickupPivotSmall;
        [SerializeField] private Transform pickupPivotBig;
        
        [SerializeField] private LayerMask pickupMask;

        public bool picked => pickedObject != null;
        
        public void TogglePick()
        {
            if (picked)
                TryDrop();
            else
                TryPick();
        }
        
        public void TryPick()
        {
            float SqrDist(Transform left, Transform right) => (left.position - right.position).sqrMagnitude;
            
            if (picked)
                return;
            
            
            var count = pickupTrigger.OverlapCollider(new ContactFilter2D { layerMask = pickupMask }, collidersInPickupZone);
            if (count == 0)
                return;

            var closest = collidersInPickupZone[0];
            Pickable closestPickable = null;
            
            for (int i = 0; i < count; i++)
                if (SqrDist(transform, collidersInPickupZone[i].transform) <=
                    SqrDist(transform, closest.transform) &&
                    collidersInPickupZone[i].TryGetComponent(out Pickable pickable))
                {
                    closest = collidersInPickupZone[i];
                    closestPickable = pickable;
                }

            if (!closestPickable)
                return;
            
            pickedObject = closestPickable;
            pickedObject.Pick(pickedObject.size == PickableSize.Small ? pickupPivotSmall : pickupPivotBig,
                                pickedObject.size == PickableSize.Small ? pickupPivotSmall : pickupPivotBig);
        }

        public void TryDrop()
        {
            if (!picked)
                return;
            
            pickedObject.Drop();
        }

        public void ForceDropObject()
        {
            pickedObject = null;
        }

        public void ToggleInteraction()
        {
            if (!picked)
                return;
            pickedObject.activated = !pickedObject.activated;
            
            if (pickedObject.activated)
                pickedObject.StartInteraction();
            else
                pickedObject.StopInteraction();
        }
    }
}