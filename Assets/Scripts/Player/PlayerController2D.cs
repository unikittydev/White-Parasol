using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PickHandler), typeof(GroundChecker2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private KeyCode jumpCode = KeyCode.Space;
        [SerializeField] private KeyCode pickupCode = KeyCode.Return;
        [SerializeField] private KeyCode interactCode = KeyCode.E;
        
        [Header("Characteristics")]
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float jumpHeight = 5f;

        [SerializeField] private UnityEvent onJump;
        
        public float viewDirection { get; private set; } = 1f;
        public float move { get; private set; }
        private bool jump;

        private bool2 overrideVelocityFlag;
        private Vector2 overrideVelocity;

        private Rigidbody2D _rb;
        public Rigidbody2D rb => _rb;
        private GroundChecker2D gc;

        private PickHandler _pickHandler;
        public PickHandler pickHandler => _pickHandler;
        
        private PlayerState currentState;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            gc = GetComponent<GroundChecker2D>();
            _pickHandler = GetComponent<PickHandler>();
        }

        private void Update()
        {
            GetInput();
            PickupOrDrop();
            Interact();
        }

        private void GetInput()
        {
            move = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(move) > .5f)
                viewDirection = move;
            
            if (!jump)
                jump = Input.GetKeyDown(jumpCode);
        }

        private void PickupOrDrop()
        {
            if (!Input.GetKeyDown(pickupCode))
                return;
            pickHandler.TogglePick();
        }

        private void Interact()
        {
            if (!Input.GetKeyDown(interactCode))
                return;
            pickHandler.ToggleInteraction();
        }

        private void FixedUpdate()
        {
            Walk();
            Jump();
            ApplyOverridenVelocity();
        }

        private void Walk()
        {
            var velocity = new Vector2(move * movementSpeed, rb.velocity.y);
            rb.velocity = velocity;
        }
        
        private void Jump()
        {
            if (!jump)
                return;
            jump = false;
            
            if (!gc.isGrounded)
                return;
            
            Vector2 velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight));
            rb.velocity = velocity;
            onJump?.Invoke();
        }
        
        private void ApplyOverridenVelocity()
        {
            Vector2 velocity = rb.velocity;
            if (overrideVelocityFlag.x)
                velocity.x = overrideVelocity.x;
            if (overrideVelocityFlag.y)
                velocity.y = overrideVelocity.y;
            rb.velocity = velocity;
            overrideVelocityFlag = false;
        }

        public void OverrideVelocityX(float value)
        {
            overrideVelocityFlag.x = true;
            overrideVelocity.x = value;
        }
        
        public void OverrideVelocityY(float value)
        {
            overrideVelocityFlag.y = true;
            overrideVelocity.y = value;
        }
        
        public void SetState<T>() where T : PlayerState, new()
        {
            if (currentState)
                Destroy(currentState);
            currentState = gameObject.AddComponent<T>();
        }
    }
}