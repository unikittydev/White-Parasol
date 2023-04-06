using UnityEngine;

namespace Game
{
    public class BossState : MonoBehaviour
    {
        [SerializeField] private float _weight;
        public float weight => _weight;
    }
}