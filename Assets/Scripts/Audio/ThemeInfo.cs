using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Theme_", menuName = "Audio/Theme")]
    public class ThemeInfo : ScriptableObject
    {
        public AudioClip clip;
        public float pitch;
        public float maxVolume;
    }
}
