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

        public override void OnStateStay()
        {
            float h = Input.GetAxisRaw("Horizontal"); // A / D
            float v = Input.GetAxisRaw("Vertical");   // W / S

            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                player.stateMachine.SwitchState(PlayerStates.MOVE);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.stateMachine.SwitchState(PlayerStates.JUMP);
            }
        }
    }
}