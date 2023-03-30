using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private UnityEvent onButtonEnable;
        [SerializeField] private UnityEvent onButtonDisable;

        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;

        [SerializeField] private AudioSource onSound;
        [SerializeField] private AudioSource offSound;
        
        private SpriteRenderer renderer;

        private List<Collider2D> pushers = new();

        private bool _buttonEnabled;
        public bool buttonEnabled => _buttonEnabled;
        
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            onButtonEnable.AddListener(onSound.Play);
            onButtonDisable.AddListener(offSound.Play);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger)
                return;
            pushers.Add(col);
            renderer.sprite = onSprite;
            _buttonEnabled = true;
            
            if (enabled)
                onButtonEnable?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.isTrigger)
                return;
            pushers.Remove(col);
            if (pushers.Count > 0)
                return;
            renderer.sprite = offSprite;
            _buttonEnabled = false;
            
            if (enabled)
                onButtonDisable?.Invoke();
        }
    }
}
