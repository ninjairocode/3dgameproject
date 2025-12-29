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
            player.transform.localScale = new Vector3(1.1f, 0.8f, 1.1f);

        }

        public override void OnStateStay()
        {
            if (!jumped)
            {
                Vector3 vel = player.rb.linearVelocity;
                vel.y = player.jumpForce;
                player.rb.linearVelocity = vel;

                jumped = true;
                player.transform.localScale = new Vector3(0.9f, 1.2f, 0.9f);

            }

            
            if (player.rb.linearVelocity.y <= 0)
            {
                player.transform.localScale = Vector3.one;
                player.stateMachine.SwitchState(PlayerStates.IDLE);
            }
        }
    }
}