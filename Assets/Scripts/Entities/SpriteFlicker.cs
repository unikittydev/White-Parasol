using System.Collections;
using UnityEngine;

namespace Game
{
    public class SpriteFlicker : MonoBehaviour
    {
        [SerializeField] private float frequency;
        [SerializeField] private float flickerTime;
        [SerializeField] private Color flickerTint;

        [SerializeField] private SpriteRenderer[] renderers;
        private Color[] defaultTint;

        private void Awake()
        {
            defaultTint = new Color[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
                defaultTint[i] = renderers[i].color;
        }

        public void Flicker()
        {
            StartCoroutine(_Flicker());
        }

        private IEnumerator _Flicker()
        {
            float counter = 0f, flickerCounter = 0f;

            bool tinted = false;
            
            while (counter < flickerTime)
            {
                if (flickerCounter >= frequency)
                {
                    tinted = !tinted;
                    Flick(tinted);
                    flickerCounter -= frequency;
                }

                counter += Time.deltaTime;
                flickerCounter += Time.deltaTime;
                yield return null;
            }
            Flick(false);
        }

        private void Flick(bool tint)
        {
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].color = tint ? flickerTint : defaultTint[i];
        }
    }
}