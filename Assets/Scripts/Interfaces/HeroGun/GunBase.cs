using System.Collections;
using UnityEngine;

namespace Interfaces.HeroGun
{
    public class GunBase : MonoBehaviour
    {
        public ProjectileBase prefabProjectile;

        public Transform positionToShoot;

        public float timeBetweenShoot = .3f;
        
        public float speed = 50f;

        

        //public AudioSource gunAudio;

        private Coroutine _currentCoroutine;


        

        protected virtual IEnumerator StartShoot()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }


        public virtual void Shoot()
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = positionToShoot.position;
            projectile.transform.rotation = transform.rotation;
            projectile.speed = speed;
            
        }

        public void Shooting()
        {
            StopShoot();
            _currentCoroutine = StartCoroutine(StartShoot());
        }

        public void StopShoot()
        {
            if (_currentCoroutine !=null)  
                StopCoroutine(_currentCoroutine);
        }
    
    }
}
