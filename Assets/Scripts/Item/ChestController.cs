using Audio;
using Save;
using UnityEngine;

namespace Item
{
    public class ChestController : MonoBehaviour
    {
        [Header("ID Único do Baú")]
        public string chestID;
        
        [Header("Animator do Baú")]
        public Animator animator;
        public Transform spawnPoint;
        public int coinAmount = 10;

        [Header("UI")]
        public GameObject openHintUI;

        private bool isPlayerInside = false;
        private bool opened = false;

        private EnemyDropCoins dropper;

        private void Start()
        {
            if (openHintUI != null)
                openHintUI.SetActive(false);

            dropper = GetComponent<EnemyDropCoins>();

            if (animator != null)
            {
                animator.ResetTrigger("Open");
                animator.Play("Idle", 0, 0f); // força o estado inicial
            }
        }

        private void Update()
        {
            if (isPlayerInside && !opened)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    OpenChest();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (opened) return;

            if (other.CompareTag("Player"))
            {
                isPlayerInside = true;
                if (openHintUI != null)
                    openHintUI.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInside = false;
                if (openHintUI != null)
                    openHintUI.SetActive(false);
            }
        }

        private void OpenChest()
        {
            opened = true;
            
            GameWorldState.Instance.RegisterChestOpened(chestID);
            
            SoundManager.Instance.PlaySFX("chest");



            if (openHintUI != null)
                openHintUI.SetActive(false);

            
            if (animator != null)
                animator.SetTrigger("Open");

            CallDropCoins();
        }

        private void CallDropCoins()
        {
            if (dropper == null) return;

            Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;

            dropper.DropCoinsAt(pos, coinAmount);
        }
        
        public void ApplySavedState(bool wasOpened)
        {
            opened = wasOpened;

            if (opened)
            {
                isPlayerInside = false;
                if (openHintUI != null)
                    openHintUI.SetActive(false);

                if (animator != null)
                {
                    animator.ResetTrigger("Open");
                    animator.Play("Open", 0, 1f);
                }
                
                var col = GetComponent<Collider>();
                if (col != null)
                    col.enabled = false;

            }
        }
    }
}