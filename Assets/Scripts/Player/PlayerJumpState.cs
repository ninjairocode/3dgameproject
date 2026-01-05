using States;
using UnityEngine;

namespace Player
{
    public class PlayerJumpState : StateBase
    {
        private PlayerController player;

        public PlayerJumpState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            if (player == null || player.rb == null) return;

            
            if (player.transform != null)
                player.transform.localScale = new Vector3(1.1f, 0.8f, 1.1f);

           
            if (player.anim != null)
                player.anim.SetTrigger("Jump");

            
            Vector3 vel = player.rb.linearVelocity;
            vel.y = player.jumpForce;
            player.rb.linearVelocity = vel;
        }

        public override void OnStateStay()
        {
            if (player == null || player.rb == null) return;

            
            if (player.rb.linearVelocity.y <= 0f)
            {
                if (player.transform != null)
                    player.transform.localScale = Vector3.one;

                player.stateMachine.SwitchState(PlayerStates.MOVE);
            }
        }
    }
}