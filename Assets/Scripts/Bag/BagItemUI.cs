using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
    public class BagItemUI : MonoBehaviour
    {
        public enum ItemType { Heal, Power }
        
        [Header("ID Único do Item")]
        public string itemID;


        public ItemType itemType;
        public int quantity = 3;

        public float cooldown = 5f;
        private float cooldownTimer = 0f;

        public TextMeshProUGUI quantityText;
        public TextMeshProUGUI cooldownText;
        public Button button;

        public BagActions bagActions;

        private void Start()
        {
            UpdateUI();
            button.onClick.AddListener(OnClick);
        }

        private void Update()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
                cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();
                button.interactable = false;
            }
            else
            {
                cooldownText.text = "";
                button.interactable = quantity > 0;
            }
        }

        private void UpdateUI()
        {
            quantityText.text = quantity.ToString();
        }

        private void OnClick()
        {
            if (quantity <= 0 || cooldownTimer > 0) return;

            switch (itemType)
            {
                case ItemType.Heal:
                    bagActions.UseHeal();
                    break;

                case ItemType.Power:
                    bagActions.UsePower();
                    break;
            }

            quantity--;
            UpdateUI();

            cooldownTimer = cooldown;
        }
        
        public float GetCooldownTimer()
        {
            return cooldownTimer;
        }

        public void ApplySave(int savedQuantity, float savedCooldown)
        {
            quantity = savedQuantity;
            cooldownTimer = savedCooldown;
            UpdateUI();
        }

    }
}