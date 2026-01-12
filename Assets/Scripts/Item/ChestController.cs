// using UnityEngine;
//
// namespace Item
// {
//     public class ChestController : MonoBehaviour
//     {
//         [Header("Animator do Baú")]
//         public Animator animator;
//         public Transform spawnPoint;
//         public int coinAmount = 10;
//
//         [Header("UI")]
//         public GameObject openHintUI;
//
//         private bool isPlayerInside = false;
//         private bool opened = false;
//
//         
//         private EnemyDropCoins dropper;
//
//         private void Start()
//         {
//             if (openHintUI != null)
//                 openHintUI.SetActive(false);
//
//             if (animator != null)
//                 animator.SetBool("Opened", false);
//
//             dropper = GetComponent<EnemyDropCoins>();
//         }
//
//         private void Update()
//         {
//             if (isPlayerInside && !opened)
//             {
//                 if (Input.GetKeyDown(KeyCode.Y))
//                 {
//                     OpenChest();
//                 }
//             }
//         }
//
//         private void OnTriggerEnter(Collider other)
//         {
//             if (opened) return;
//
//             if (other.CompareTag("Player"))
//             {
//                 isPlayerInside = true;
//                 if (openHintUI != null)
//                     openHintUI.SetActive(true);
//             }
//         }
//
//         private void OnTriggerExit(Collider other)
//         {
//             if (other.CompareTag("Player"))
//             {
//                 isPlayerInside = false;
//                 if (openHintUI != null)
//                     openHintUI.SetActive(false);
//             }
//         }
//
//         private void OpenChest()
//         {
//             opened = true;
//
//             if (openHintUI != null)
//                 openHintUI.SetActive(false);
//
//             if (animator != null)
//                 animator.SetBool("Opened", true);
//
//             CallDropCoins();
//         }
//
//         private void CallDropCoins()
//         {
//             if (dropper == null) return;
//
//             
//             Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
//
//             dropper.DropCoinsAt(pos, coinAmount);
//         }
//     }
// }

using UnityEngine;

namespace Item
{
    public class ChestController : MonoBehaviour
    {
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

            if (openHintUI != null)
                openHintUI.SetActive(false);

            // Agora usando TRIGGER
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
    }
}