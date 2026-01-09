using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewCoin", menuName = "coinData")]
    public class SoCoin : ScriptableObject
    {
       public int coinValue = 1;
    }
}
