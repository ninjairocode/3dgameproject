using System.Collections;
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

        [Header("Components")]
        public Rigidbody rb;
        public Animator anim;

        [Header("FLASH")]
        public List<FlashColor> flashColors;

        

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();

            stateMachine = new StateMachine<PlayerStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerIdleState(this));
            stateMachine.RegisterStates(PlayerStates.MOVE, new PlayerMoveState(this));
            stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerJumpState(this));
            stateMachine.RegisterStates(PlayerStates.SPRINT, new SprintState(this));

            stateMachine.SwitchState(PlayerStates.IDLE);

            currentSpeed = walkSpeed;

            
            if (rb == null)
                rb = GetComponent<Rigidbody>();

            if (rb != null)
                rb.isKinematic = false;
        }

        private void Update()
        {
            stateMachine.Update();
        }

        #region LIFE

        public void Damage(float damage)
        {
            flashColors.ForEach(i => i.Flash());
        }

        public void Damage(float damage, Vector3 dir)
        {
            Damage(damage);

            
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