using UnityEngine;

namespace Game
{
    public class ToggleAnimationInteraction : MonoBehaviour
    {
        private static int STATE_USED = Animator.StringToHash("STATE_USED");

        [SerializeField] private Animator animator;
        
        public void SetState(bool used)
        {
            animator.SetBool(STATE_USED, used);
        }
    }
}
