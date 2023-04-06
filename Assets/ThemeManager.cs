using System.Collections;
using UnityEngine;
using UnityEngine.Search;

namespace Game
{
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField] private AudioSource themeSource;
        [SerializeField] private float fadeOut;
        
        [SerializeField] private float fadeIn;

        [SerializeField] private float maxVolume = 1f;
        [SerializeField] private float waitDelay;

        private ThemeInfo currentTheme;
        
        public void SwitchTheme(ThemeInfo theme)
        {
            if (currentTheme != theme)
                StartCoroutine(Switch(theme));
        }

        private IEnumerator Switch(ThemeInfo theme)
        {
            float counter = 0f;
            float volumeFactor = currentTheme?.maxVolume ?? 1f;
            while (counter < fadeOut)
            {
                themeSource.volume = Mathf.Lerp(maxVolume * volumeFactor, 0f, counter / fadeOut);
                counter += Time.deltaTime;
                yield return null;
            }
            
            themeSource.Stop();
            yield return new WaitForSeconds(waitDelay);

            themeSource.clip = theme.clip;
            themeSource.pitch = theme.pitch;
            currentTheme = theme;
            themeSource.Play();
            
            counter = 0f;
            while (counter < fadeIn)
            {
                themeSource.volume = Mathf.Lerp(0f, maxVolume * theme.maxVolume, counter / fadeIn);
                counter += Time.deltaTime;
                yield return null;
            }
        }
    }
}
