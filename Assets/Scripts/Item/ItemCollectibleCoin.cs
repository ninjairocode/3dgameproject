using Audio;
using UnityEngine;

namespace Item
{
    public class ItemCollectibleCoin : ItemCollectibleBase
    {
        public SoCoin coinData;
        protected override void OnCollect()
        {
            base.OnCollect();
            Debug.Log("Collected Coin");
            SoundManager.Instance.PlaySFX("coin");


            if (coinData != null)
            {
                ItemManager.Instance.AddCoins(coinData);
            }
            else
            {
                Debug.LogWarning("CoinData faltando");
            }
            
        }
    }
}
