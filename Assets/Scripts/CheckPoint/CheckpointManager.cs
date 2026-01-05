using System.Collections.Generic;
using UnityEngine;

namespace CheckPoint
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;

        [Header("Lista de Checkpoints")]
        public List<GameObject> checkpoints = new List<GameObject>();

        [Header("Último Checkpoint Ativado")]
        public Transform lastCheckpoint;

        private void Awake()
        {
            Instance = this;
        }

        public void ActivateCheckpoint(GameObject cp)
        {
            lastCheckpoint = cp.transform;

            foreach (GameObject c in checkpoints)
            {
                var checkpoint = c.GetComponent<Checkpoint>();
                if (checkpoint != null)
                    checkpoint.SetActive(c == cp);
            }
        }

        public Vector3 GetRespawnPosition()
        {
            return lastCheckpoint != null ? lastCheckpoint.position : Vector3.zero;
        }
    }
}
