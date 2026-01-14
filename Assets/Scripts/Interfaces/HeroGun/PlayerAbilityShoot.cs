using System.Collections.Generic;
using Audio;
using Player;
using TMPro;
using UnityEngine;

namespace Interfaces.HeroGun
{
    public class PlayerAbilityShoot : PlayerAbilityBase
    {
        [Header("Gun Settings")]
        public List<GunBase> gunPrefabs;
        public Transform gunPosition;
        
        [Header("UI")]
        public TMP_Text weaponText;

        private GunBase _currentGun;
        private int _currentGunIndex = 0;

        protected override void Init()
        {
            base.Init();

            EquipGun(_currentGunIndex);

            
            inputs.Gameplay.Shoot.performed += ctx => Shoot();
            inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

            
            inputs.Gameplay.Weapon1.performed += ctx => ChangeGun(0);
            inputs.Gameplay.Weapon2.performed += ctx => ChangeGun(1);
            inputs.Gameplay.Weapon3.performed += ctx => ChangeGun(2);
        }

        private void EquipGun(int index)
        {
            if (index < 0 || index >= gunPrefabs.Count)
            {
                Debug.LogWarning("Índice de arma inválido");
                return;
            }

            
            if (_currentGun != null)
                Destroy(_currentGun.gameObject);

            
            _currentGun = Instantiate(gunPrefabs[index], gunPosition);
            _currentGun.transform.localPosition = Vector3.zero;
            _currentGun.transform.localEulerAngles = Vector3.zero;

            _currentGunIndex = index;

            
            if (weaponText != null)
                weaponText.text = gunPrefabs[index].name;

            Debug.Log($"Arma equipada: {gunPrefabs[index].name}");
        }

        private void ChangeGun(int index)
        {
            if (index == _currentGunIndex) return;
            EquipGun(index);
        }

        private void Shoot()
        {
            if (_currentGun == null) return;

            _currentGun.Shooting();
            SoundManager.Instance.PlaySFX("gun");

            Debug.Log("Shooting");
        }

        private void CancelShoot()
        {
            if (_currentGun == null) return;

            Debug.Log("Shoot Canceled");
            _currentGun.StopShoot();
        }
    }
}