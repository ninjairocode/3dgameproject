using Player;
using UnityEngine;

namespace Bag
{
    public class BagActions : MonoBehaviour
    {
        public static BagActions Instance;

        public float healAmount = 30f;

        public int powerBonus = 2;
        public float powerDuration = 10f;

        private void Awake()
        {
            Instance = this;
        }

        public void UseHeal()
        {
            var player = GameObject.FindWithTag("Player").GetComponent<Player.PlayerController>();

            if (player != null && !player.isDead)
            {
                player.currentLife += healAmount;

                if (player.currentLife > player.maxLife)
                    player.currentLife = player.maxLife;

                PlayerUI.Instance.UpdateLifeBar(player.currentLife, player.maxLife);
            }
        }

        public void UsePower()
        {
            StartCoroutine(ProjectilePowerManager.ApplyPower(powerBonus, powerDuration));
        }
    }
}