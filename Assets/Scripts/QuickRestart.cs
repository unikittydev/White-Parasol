using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class QuickRestart : MonoBehaviour
    {
        [SerializeField] private KeyCode restartKey;
        [SerializeField] private float holdTime;
        [SerializeField] private float doubleClickTime;

        [SerializeField]
        private float holdCounter;

        [SerializeField] private float doubleClickCounter;

        [SerializeField] private UnityEvent onDoubleClick;
        
        private int clickCount;
        
        private void Update()
        {
            // Respawn
            if (Input.GetKeyDown(restartKey))
                clickCount++;
            if (clickCount > 0 && doubleClickCounter < doubleClickTime)
                doubleClickCounter += Time.deltaTime;
            if (doubleClickCounter >= doubleClickTime)
            {
                if (clickCount >= 2)
                    onDoubleClick?.Invoke();

                doubleClickCounter = 0f;
                clickCount = 0;
            }
            
            if (Input.GetKey(restartKey))
                holdCounter += Time.deltaTime;
            else
                holdCounter = 0f;
            
            if (holdCounter > holdTime)
                ReloadLevel();
        }

        private void ReloadLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
