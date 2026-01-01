using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroGun
{
    public class GunShootLimit : GunBase
    {
        public float maxShoot = 5;
        public float timeToRecharge = 1f;

        private float _currentShoots;
        private bool _isRecharging = false;


        protected override IEnumerator StartShoot()
        {
            if(_isRecharging) yield break;
            
            while (true)
            {
                if (_currentShoots < maxShoot)
                {
                    Shoot();
                    _currentShoots++;
                    CheckRecharge();
                    yield return new WaitForSeconds(timeBetweenShoot);
                }
            }
        }

        private void CheckRecharge()
        {
            if (_currentShoots < maxShoot)
            {
                StopShoot();
                Recharge();
            }
        }
        
        public void Recharge()
        {
            _isRecharging = true;
            StartCoroutine(RechargeCoroutine());
        }
        
        IEnumerator RechargeCoroutine()
        {
            float time = 0;
            while (time < timeToRecharge)
            {
                time += Time.deltaTime;
                Debug.Log("Recharging: " + time);
                yield return new WaitForEndOfFrame();
            }
            _currentShoots = 0;
            _isRecharging = false;
        }
    }
}
