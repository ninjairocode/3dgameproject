using States;
using UnityEngine;

namespace Player
{
    public class PlayerJumpState : StateBase
    {
        private PlayerController player;
        private bool jumped = false;

        public PlayerJumpState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            jumped = false;
        }

        public override void OnStateStay()
        {
            if (!jumped)
            {
                Vector3 vel = player.rb.linearVelocity;
                vel.y = player.jumpForce;
                player.rb.linearVelocity = vel;

                jumped = true;
            }

            // Quando começar a cair → Idle
            if (player.rb.linearVelocity.y <= 0)
            {
                player.stateMachine.SwitchState(PlayerStates.IDLE);
            }
        }
    }
}