using System.Collections.Generic;
using Interfaces;
using States;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public StateMachine<PlayerStates> stateMachine;

        [Header("Movement Settings")]
        public float walkSpeed = 5f;
        public float sprintSpeed = 9f;
        public float currentSpeed = 5f;
        public float jumpForce = 7f;
        public float rotationSpeed = 180f;

        [Header("Life Settings")]
        public float maxLife = 100f;
        public float currentLife;
        public bool isDead = false;

        [Header("Components")]
        public Rigidbody rb;
        public Animator anim;

        [Header("FLASH")]
        public List<FlashColor> flashColors;

        private void Awake()
        {
            currentLife = maxLife;
            PlayerUI.Instance.UpdateLifeBar(currentLife, maxLife);

            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();

            if (rb != null)
                rb.isKinematic = false;

            stateMachine = new StateMachine<PlayerStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(PlayerStates.IDLE,   new PlayerIdleState(this));
            stateMachine.RegisterStates(PlayerStates.MOVE,   new PlayerMoveState(this));
            stateMachine.RegisterStates(PlayerStates.JUMP,   new PlayerJumpState(this));
            stateMachine.RegisterStates(PlayerStates.DEATH,  new PlayerDeathState(this));
            stateMachine.RegisterStates(PlayerStates.SPRINT, new SprintState(this));

            stateMachine.SwitchState(PlayerStates.IDLE);

            currentSpeed = walkSpeed;
        }

        private void Update()
        {
            stateMachine.Update();
        }

        #region LIFE

        public void Damage(float damage)
        {
            if (isDead) return;

            if (flashColors != null)
                flashColors.ForEach(i => i.Flash());

            currentLife -= damage;
            PlayerUI.Instance.UpdateLifeBar(currentLife, maxLife);

            if (currentLife <= 0)
            {
                Die();
            }
        }

        public void Damage(float damage, Vector3 dir)
        {
            Damage(damage);
            
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;

            
            stateMachine.SwitchState(PlayerStates.DEATH);

            
        }

        
        public void Respawn()
        {
            Vector3 pos = CheckPoint.CheckpointManager.Instance.GetRespawnPosition();

            rb.isKinematic = true;
            transform.position = pos;
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = false;

            currentLife = maxLife;
            PlayerUI.Instance.UpdateLifeBar(currentLife, maxLife);

            transform.localScale = Vector3.one;

            isDead = false;

            
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = true;

            stateMachine.SwitchState(PlayerStates.IDLE);
        }

        #endregion
    }

    public enum PlayerStates
    {
        IDLE,
        MOVE,
        JUMP,
        SPRINT,
        DEATH
    }
}