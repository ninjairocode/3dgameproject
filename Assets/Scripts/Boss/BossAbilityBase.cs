using UnityEngine;

namespace Boss
{
    public sealed class BossAbilityBase : MonoBehaviour
    {
        private BossController boss;

        private void OnValidate()
        {
            if (boss == null) boss = GetComponent<BossController>();
        }

        private void Start()
        {
            Init();
            OnValidate();
            RegisterListeners();
        }

        private void OnEnable()
        {
            RegisterListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void Init()
        {

        }


        private void RegisterListeners()
        {

        }


        private void RemoveListeners()
        {

        }
    }
}