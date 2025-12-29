using States;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public StateMachine<PlayerStates> stateMachine;

        public float speed = 5f;
        public float jumpForce = 7f;
        public float rotationSpeed = 180f;
        

        public Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            stateMachine = new StateMachine<PlayerStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerIdleState(this));
            stateMachine.RegisterStates(PlayerStates.MOVE, new PlayerMoveState(this));
            stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerJumpState(this));

            stateMachine.SwitchState(PlayerStates.IDLE);
        }

        private void FixedUpdate()
        {
            stateMachine.Update();
        }
    }

    public enum PlayerStates
    {
        IDLE,
        MOVE,
        JUMP
    }
}