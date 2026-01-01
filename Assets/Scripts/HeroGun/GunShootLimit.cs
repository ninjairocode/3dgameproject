using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HeroGun
{
    public class GunShootLimit : GunBase
    {
        public List<UIGunUpdater> uiGunUpdaters;
        
        public float maxShoot = 5;
        public float timeToRecharge = 1f;

        private float _currentShoots;
        private bool _isRecharging = false;
        
        private void Awake()
        {
            GetAllUIs();
        }


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
                    UpdateUI();
                    yield return new WaitForSeconds(timeBetweenShoot);
                }
            }
        }

        private void CheckRecharge()
        {
            if (_currentShoots >= maxShoot)
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
                uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
                yield return new WaitForEndOfFrame();
            }
            _currentShoots = 0;
            _isRecharging = false;
        }

        private void UpdateUI()
        {
            uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
        }

        private void GetAllUIs()
        {
            uiGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
        }
    }
}
