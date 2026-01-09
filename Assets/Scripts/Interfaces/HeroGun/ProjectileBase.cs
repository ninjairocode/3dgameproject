using System.Collections.Generic;
using Bag;
using Interfaces;
using UnityEngine;

namespace HeroGun
{
    public class ProjectileBase : MonoBehaviour
    {

        

        public float timeToDestroy = 1f;
        
        
        public int damageAmount = 1;
        public float speed = 50f;

        public List<string> tagsToHit;

        private void Awake()
        {
            damageAmount += ProjectilePowerManager.bonusDamage;
            Destroy(gameObject, timeToDestroy);
        }
        private void Update()
        {
           
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            foreach (var t in tagsToHit)
            {
                if (collision.transform.tag == t)
                {
                    var damageable = collision.transform.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        Vector3 dir = collision.transform.position - transform.position;
                        dir = -dir.normalized;
                        dir.y = 0;
                
                        damageable.Damage(damageAmount, dir);
                    }

                    break;
                }
                
                
            }
            
            
            
            Destroy(gameObject);
        }
    
    }
}
