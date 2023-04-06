using System;
using UnityEngine;

namespace Game
{
    public class PlayerAnimator2D : MonoBehaviour
    {
        private PlayerController2D controller;
        private GroundChecker2D gc;
        private Animator animator;
        private PickHandler pickHandler;
        
        [Header("Renderers")]
        [SerializeField] private SpriteRenderer top;
        [SerializeField] private SpriteRenderer bottom;

        [SerializeField] private Transform takePivotFlipper;
        [SerializeField] private Transform pickupTrigger;
        
        private static readonly int TOP_FALL = Animator.StringToHash("TOP_FALL");
        private static readonly int WALK_SPEED = Animator.StringToHash("WALK_SPEED");
        private static readonly int BOTTOM_FALL = Animator.StringToHash("BOTTOM_FALL");

        private void Awake()
        {
            controller = GetComponent<PlayerController2D>();
            gc = GetComponent<GroundChecker2D>();
            animator = GetComponent<Animator>();
            pickHandler = GetComponent<PickHandler>();
        }

        private void Update()
        {
            SetAnimationValues();
            SetDirection(controller.viewDirection > 0f);
        }

        private void SetAnimationValues()
        {
            // Hands up if in air or hold heavy item or hold activated item
            bool pickedHeavyOrInteracting = pickHandler.picked &&
                                            (pickHandler.pickedObject.size == PickableSize.Big || pickHandler.pickedObject.activated);
            animator.SetBool(TOP_FALL, !gc.isGrounded || pickedHeavyOrInteracting);
            // Top and bottom walk speed if moving
            animator.SetFloat(WALK_SPEED, Math.Abs(controller.move));
            // Legs up if in air
            animator.SetBool(BOTTOM_FALL, !gc.isGrounded);
        }
        
        private void SetDirection(bool right)
        {
            top.flipX = !right;
            bottom.flipX = !right;
            
            Vector3 takePivotScale = takePivotFlipper.localScale;
            takePivotScale.x = right ? 1f : -1f;
            takePivotFlipper.localScale = takePivotScale;

            Vector3 pickupTriggerPos = pickupTrigger.localPosition;
            pickupTriggerPos.x = Mathf.Abs(pickupTriggerPos.x) * (right ? 1f : -1f);
            pickupTrigger.localPosition = pickupTriggerPos;
        }
    }
}
