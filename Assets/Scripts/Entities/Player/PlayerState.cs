using UnityEngine;

namespace Game
{
    public abstract class PlayerState : MonoBehaviour
    {
        protected PlayerController2D controller;

        private void Awake()
        {
            controller = GetComponent<PlayerController2D>();
        }
    }
}