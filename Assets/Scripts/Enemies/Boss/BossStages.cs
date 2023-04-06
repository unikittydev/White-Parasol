using System;
using UnityEngine;

namespace Game
{
    public class BossStages : MonoBehaviour
    {
        [SerializeField] private BossStateMachine[] stages;

        private BossStateMachine currentStage;
        
        private int stage = 0;
        
        [SerializeField]
        private Damageable damageable;

        private void Start()
        {
            currentStage = stages[0];
        }

        public void UpdateStage()
        {
            if (!enabled) return;
            
            if (damageable.currentHealth <= currentStage.completeStageOnHealth)
            {
                currentStage.CompleteStage();
                if (++stage == stages.Length)
                {
                    // Probably onBossDie?
                    enabled = false;
                    return;
                }

                currentStage = stages[stage];
                currentStage.gameObject.SetActive(true);
            }
        }
    }
}
