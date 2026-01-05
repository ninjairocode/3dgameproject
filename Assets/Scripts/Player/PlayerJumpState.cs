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
            if (player != null && player.transform != null)
                player.transform.localScale = new Vector3(1.1f, 0.8f, 1.1f);
        }

        public override void OnStateStay()
        {
            if (player == null || player.rb == null) return;

            

            // Se ainda não pulou, aplica o impulso vertical
            if (!jumped)
            {
                Vector3 vel = player.rb.linearVelocity;

                // Só aplica o salto se estiver praticamente no chão (evita double jump)
                if (Mathf.Abs(vel.y) < 0.01f)
                {
                    vel.y = player.jumpForce;
                    player.rb.linearVelocity = vel;

                    jumped = true;

                    if (player.transform != null)
                        player.transform.localScale = new Vector3(0.9f, 1.2f, 0.9f);
                }
                else
                {
                    // já está subindo/caindo, marca como pulado para não reaplicar
                    jumped = true;
                }
            }

            // Quando começar a cair (vel.y <= 0), volta para IDLE (ou outro estado de aterrissagem)
            if (player.rb.linearVelocity.y <= 0f)
            {
                if (player.transform != null)
                    player.transform.localScale = Vector3.one;

                player.stateMachine.SwitchState(PlayerStates.IDLE);
            }
        }
    }
}