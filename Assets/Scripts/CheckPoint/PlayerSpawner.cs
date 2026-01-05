// using UnityEngine;
//
// namespace CheckPoint
// {
//     public class PlayerSpawner : MonoBehaviour
//     {
//         [Header("Prefab do Player")]
//         public GameObject playerPrefab;
//
//         private GameObject currentPlayer;
//
//         private void Start()
//         {
//             SpawnPlayerAtCheckpoint();
//         }
//
//         public void SpawnPlayerAtCheckpoint()
//         {
//             
//             if (currentPlayer != null)
//                 Destroy(currentPlayer);
//
//             
//             Vector3 spawnPos = CheckpointManager.Instance.GetRespawnPosition();
//
//             
//             currentPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
//         }
//     }
// }
