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
            Instance = this;
        }

        public void UpdateLifeBar(float current, float max)
        {
            lifeFill.fillAmount = current / max;
        }
    }
}