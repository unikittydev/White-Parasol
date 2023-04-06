using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BossStateMachine : MonoBehaviour
    {
        [SerializeField] private BossState[] states;

        [SerializeField] private BossState currentState;
        [SerializeField] private bool randomStateOnStart;
        
        private float[] cumulativeWeights;

        [SerializeField]
        private float _completeStageOnHealth;
        public float completeStageOnHealth => _completeStageOnHealth;
        
        private UnityEvent onStageComplete;
        
        private void Start()
        {
            cumulativeWeights = new float[states.Length];
            float sum = 0f;
            for (int i = 0; i < states.Length; i++)
            {
                sum += states[i].weight;
                cumulativeWeights[i] = sum;
            }
            
            if (randomStateOnStart)
                ChooseNextRandomState();
            else
                SetActiveState();
        }

        public void ChooseNextRandomState()
        {
            float weightValue = Random.Range(0f, cumulativeWeights[^1]);
            
            for (int i = 0; i < cumulativeWeights.Length; i++)
                if (weightValue < cumulativeWeights[i])
                {
                    currentState = states[i];
                    break;
                }
            
            SetActiveState();
        }

        public void SetState(BossState state)
        {
            currentState = state;
            SetActiveState();
        }

        private void SetActiveState()
        {
            foreach (BossState state in states)
                state.gameObject.SetActive(false);
            
            currentState.gameObject.SetActive(true);
        }

        public void CompleteStage()
        {
            onStageComplete?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
