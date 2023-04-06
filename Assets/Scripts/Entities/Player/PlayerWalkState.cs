using UnityEngine;

namespace Game
{
    public class PlayerWalkState : PlayerState
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                controller.SetState<PlayerJumpState>();
        }
    }
}
