using UnityEngine;

namespace Item
{
    public class EnemyDropCoins : MonoBehaviour
    {
        public GameObject coinPrefab;
        public int minCoins = 1;
        public int maxCoins = 5;

        public float dropRadius = 1.5f;

        public void DropCoins()
        {
            int amount = Random.Range(minCoins, maxCoins + 1);

            for (int i = 0; i < amount; i++)
            {
                
                Vector2 randomCircle = Random.insideUnitCircle * dropRadius;

                Vector3 dropPos = transform.position + new Vector3(
                    randomCircle.x,
                    0.5f,
                    randomCircle.y
                );

                Instantiate(coinPrefab, dropPos, Quaternion.identity);
            }
        }
    }
}