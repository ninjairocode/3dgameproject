using UnityEngine;

namespace CheckPoint
{
    public class Checkpoint : MonoBehaviour
    {
        private Renderer rend;
        public Color inactiveColor = Color.gray;
        public Color activeColor = Color.green;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            SetActive(false);
        }

        public void SetActive(bool active)
        {
            if (rend != null)
                rend.material.color = active ? activeColor : inactiveColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CheckPoint.CheckpointManager.Instance.ActivateCheckpoint(gameObject);
            }
        }
    }
}