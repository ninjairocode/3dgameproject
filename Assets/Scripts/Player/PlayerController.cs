using States;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
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
        }

        private void Update()
        {
            stateMachine.Update();
        }
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