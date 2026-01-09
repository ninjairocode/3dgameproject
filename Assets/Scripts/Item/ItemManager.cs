using EBAC.Core.Singleton;
using UnityEngine;

namespace Item
{
    public class ItemManager : Singleton<ItemManager>
    {
        public TMPro.TextMeshProUGUI countCoins;

        public int coinsQuantity = 0;

        private void Start()
        {
            UpdateUI();
        }

        public void AddCoins(SoCoin coin)
        {
            coinsQuantity += coin.coinValue;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (countCoins != null)
                countCoins.text = coinsQuantity.ToString();
        }
    }
}


