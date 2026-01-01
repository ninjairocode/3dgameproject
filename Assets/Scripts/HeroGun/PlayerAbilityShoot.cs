using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HeroGun
{
    public class PlayerAbilityShoot : PlayerAbilityBase
    {

        public GunBase gunBase;
        public Transform gunPosition;

        private GunBase _currentGun;
        

        protected override void Init()
        {
            base.Init();
            
            CreateGun();
            
            inputs.Gameplay.Shoot.performed += ctx => Shoot();
            inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        }
        
        private void CreateGun()
        {
            _currentGun = Instantiate(gunBase, gunPosition);
            
            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }

        
        private void Shoot()
        {
            _currentGun.Shooting();
            Debug.Log("Shooting");
        }

        private void CancelShoot()
        {
            Debug.Log("Shoot Canceled");
            _currentGun.StopShoot();
        }
    }
}
