using System.Collections;
using UnityEngine;

namespace Game
{
    public class ChainedBlockDoor : MonoBehaviour
    {
        [SerializeField] private GameObject[] doorTiles;

        [SerializeField] private bool openedByDefault;
        [SerializeField] private float openSpeed;

        [SerializeField] private AudioSource doorOpenSound;
        [SerializeField] private AudioSource doorCloseSound;
        
        private Coroutine doorCoroutine;
        
        private bool opened;

        private void Start()
        {
            if (openedByDefault)
                Open();
            else
                Close();
        }

        public void Open()
        {
            if (doorCoroutine != null)
                StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(SetChainActive(true));
        }

        public void Close()
        {
            if (doorCoroutine != null)
                StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(SetChainActive(false));
        }

        public void Toggle()
        {
            if (opened)
                Close();
            else
                Open();
        }

        private IEnumerator SetChainActive(bool active)
        {
            opened = active;
            
            if (!opened)
                for (int i = 0; i < doorTiles.Length; i++)
                {
                    doorTiles[i].SetActive(true);
                    doorOpenSound.Play();
                    yield return new WaitForSeconds(openSpeed);
                }
            else
                for (int i = doorTiles.Length - 1; i >= 0; i--)
                {
                    doorTiles[i].SetActive(false);
                    doorCloseSound.Play();
                    yield return new WaitForSeconds(openSpeed);
                }
        }
    }
}
