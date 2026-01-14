using DG.Tweening;
using UnityEngine;
using Animation;
using Audio;
using Interfaces;
using Item;
using Player;
using Save;
using Utils;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("ID Único do Inimigo")]
        public string enemyID;
        
        [Header("Components")]
        public Collider bodyCollider;
        public FlashColor flashColor;
        public ParticleSystem particles;

        [Header("Life")]
        public float startLife = 10f;
        public float currentLife; 
        public AnimationBase animationBase;
        
        [Header("Behavior")]
        public bool lookAtPlayer = false;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        
        public Rigidbody rb;
        public PlayerController playerController;
        
        private bool alreadyDead = false;

        private void OnValidate()
        {
            
            if (bodyCollider == null)
                bodyCollider = GetComponent<Collider>();

            if (animationBase == null)
                animationBase = GetComponentInChildren<AnimationBase>();
        }

        private void Awake()
        {
            
            rb = GetComponent<Rigidbody>();
            if (animationBase == null)
                animationBase = GetComponentInChildren<AnimationBase>();

            Init();
        }

        private void Start()
        {
            
            if (playerController == null)
                playerController = GameObject.FindObjectOfType<PlayerController>();
        }

        public void ResetLife()
        {
            currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation)
            {
                BornAnimation();
            }
        }

        protected virtual void Kill()
        {
            Debug.Log($"[EnemyBase:{name}] Kill() chamado. currentLife = {currentLife}");
            GameWorldState.Instance.RegisterEnemyDefeated(enemyID);

            OnKill();

            
            float deathAnimationDuration = 1.5f;

            Debug.Log($"[EnemyBase:{name}] Destroy em {deathAnimationDuration} segundos");
            Destroy(gameObject, deathAnimationDuration);
        }



       
        protected virtual void OnKill()
        {
            if (bodyCollider != null) bodyCollider.enabled = false;
            
            var drop = GetComponent<EnemyDropCoins>();
            if (drop != null)
            {
                drop.DropCoins();
            }


            
            if (animationBase != null)
            {
                PlayAnimationByTrigger(AnimationType.DEATH);
            }
            else
            {
                
                Animator a = GetComponentInChildren<Animator>();
                if (a != null) a.SetTrigger("Death");
            }

            
        }

        public virtual void OnDamage(float damage)
        {
            
            if (flashColor != null) flashColor.Flash();
            if (particles != null) particles.Play();
            SoundManager.Instance.PlaySFX("damage");

            

            currentLife -= damage;
            if (currentLife <= 0f)
            {
                Kill();
            }
        }

        #region ANIMATION

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            if (animationBase == null)
            {
                Debug.LogWarning($"[EnemyBase] AnimationBase não atribuído em {name}. Ignorando PlayAnimationByTrigger({animationType}).");
                return;
            }

            animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        public virtual void Damage(float damage)
        {
            Debug.Log($"Damage {damage} on {name}");
            OnDamage(damage);
            
        }

        public virtual void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            

            
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerController p = collision.transform.GetComponent<PlayerController>();
            if (p != null)
            {
                
                p.Damage(10);
            }
        }

        public virtual void Update()
        {
            if (lookAtPlayer && playerController != null)
            {
                
                Vector3 target = playerController.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }
        
        public void ApplySavedState(bool defeated)
        {
            if (defeated)
            {
                alreadyDead = true;
                gameObject.SetActive(false);
            }
        }
    }
}