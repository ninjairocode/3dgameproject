using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        public static PlayerUI Instance;

        public Image lifeFill;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            if (lifeFill == null)
                Debug.LogWarning("PlayerUI: lifeFill não atribuído no Inspector!");
        }

        public void UpdateLifeBar(float current, float max)
        {
            if (lifeFill == null || max <= 0f)
                return;

            lifeFill.fillAmount = current / max;
        }
    }
}