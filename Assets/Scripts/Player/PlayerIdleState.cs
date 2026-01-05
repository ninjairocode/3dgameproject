using States;
using UnityEngine;

namespace Player
{
    public class PlayerIdleState : StateBase
    {
        private PlayerController player;

        public PlayerIdleState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            if (player.anim != null)
                player.anim.SetBool("Idle", true);
        }

        public override void OnStateExit()
        {
            if (player.anim != null)
                player.anim.SetBool("Idle", false);
        }

        public override void OnStateStay()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                player.stateMachine.SwitchState(PlayerStates.MOVE);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.stateMachine.SwitchState(PlayerStates.JUMP);
            }
        }
    }
}