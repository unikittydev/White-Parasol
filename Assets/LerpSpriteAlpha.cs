using System.Collections;
using UnityEngine;

namespace Game
{
    public class LerpSpriteAlpha : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;
        
        [SerializeField] private float lerpTime;

        [SerializeField] private float from = 0f;
        [SerializeField] private float to = 1f;
        
        private IEnumerator Start()
        {
            float counter = 0f;
            while (counter < lerpTime)
            {
                var color = renderer.color;
                color.a = Mathf.Lerp(from, to, Mathf.Clamp01(counter / lerpTime));
                renderer.color = color;
                counter += Time.deltaTime;
                yield return null;
            }
        }
    }
}
