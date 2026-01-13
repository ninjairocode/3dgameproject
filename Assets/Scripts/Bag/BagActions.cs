using Cloth;
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

        private PlayerController player;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            var obj = GameObject.FindWithTag("Player");
            if (obj != null)
                player = obj.GetComponent<PlayerController>();
        }

        public void UseHeal()
        {
            if (player == null || player.isDead)
                return;

            player.currentLife = Mathf.Clamp(player.currentLife + healAmount, 0, player.maxLife);
            PlayerUI.Instance?.UpdateLifeBar(player.currentLife, player.maxLife);
        }

        public void UsePower()
        {
            if (player == null || player.isDead)
                return;
            
            player.clothChanger.ChangeCloth(ClothType.POWER);


            StartCoroutine(ProjectilePowerManager.ApplyPower(powerBonus, powerDuration));
        }
    }
}