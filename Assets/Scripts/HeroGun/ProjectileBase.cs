using Interfaces;
using UnityEngine;

namespace HeroGun
{
    public class ProjectileBase : MonoBehaviour
    {

        

        public float timeToDestroy = 1f;
        
        
        public int damageAmount = 1;
        public float speed = 50f;

        private void Awake()
        {
            Destroy(gameObject, timeToDestroy);
        }
        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.transform.GetComponent<IDamageable>();
            
            if(damageable != null) damageable.Damage(damageAmount);
            
            Destroy(gameObject);
        }
    
    }
}
