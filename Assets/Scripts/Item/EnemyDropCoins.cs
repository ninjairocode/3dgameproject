using UnityEngine;

namespace Item
{
    public class EnemyDropCoins : MonoBehaviour
    {
        [Header("Prefab e Quantidade")]
        public GameObject coinPrefab;
        public int minCoins = 1;
        public int maxCoins = 5;

        [Header("Configuração de Queda")]
        public float dropRadius = 1.5f;
        public float spawnHeight = 0.5f;
        public float force = 3f;
        public float coinLifetime = 10f;

        
        public void DropCoins()
        {
            DropCoinsAt(transform.position);
        }

        
        public void DropCoinsAt(Vector3 centerPosition, int amount = -1)
        {
            if (coinPrefab == null)
            {
                Debug.LogError("EnemyDropCoins: coinPrefab não atribuído.");
                return;
            }

            if (amount <= 0)
                amount = Random.Range(minCoins, maxCoins + 1);

            for (int i = 0; i < amount; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * dropRadius;
                Vector3 dropPos = centerPosition + new Vector3(randomCircle.x, spawnHeight, randomCircle.y);

                GameObject coin = Instantiate(coinPrefab, dropPos, Random.rotation);
                if (coin == null)
                {
                    Debug.LogError("EnemyDropCoins: Instantiate retornou null.");
                    continue;
                }

                Rigidbody rb = coin.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0.8f, Random.Range(-1f, 1f)).normalized;
                    rb.AddForce(randomDir * force, ForceMode.Impulse);
                }
                else
                {
                    Debug.LogWarning("EnemyDropCoins: prefab não tem Rigidbody; sem física aplicada.");
                }

                if (coinLifetime > 0f)
                    Destroy(coin, coinLifetime);
            }
        }
    }
}