using States;
using UnityEngine;

namespace Player
{
    public class SprintState : StateBase
    {
        private PlayerController player;

        public SprintState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            player.currentSpeed = player.sprintSpeed;
            if (player.anim != null)
            {
                player.anim.SetBool("Run", true);
                player.anim.speed = 1.5f;
            }
        }

        public override void OnStateStay()
        {
            
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(h) > 0.1f)
            {
                player.transform.Rotate(Vector3.up, h * player.rotationSpeed * Time.deltaTime);
            }

            if (player.rb == null) return;

            // Mantém a componente Y da velocidade (gravidade / knockback)
            Vector3 vel = player.rb.linearVelocity;

            if (Mathf.Abs(v) > 0.1f)
            {
                Vector3 forward = player.transform.forward;
                Vector3 move = forward * v * player.currentSpeed;

                vel.x = move.x;
                vel.z = move.z;
            }
            else
            {
                vel.x = 0f;
                vel.z = 0f;
            }

            player.rb.linearVelocity = vel;

            // Transições
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                player.currentSpeed = player.walkSpeed;
                player.stateMachine.SwitchState(PlayerStates.MOVE);
                return;
            }

            if (Mathf.Abs(v) < 0.1f)
            {
                player.currentSpeed = player.walkSpeed;
                player.stateMachine.SwitchState(PlayerStates.IDLE);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.currentSpeed = player.walkSpeed;
                player.stateMachine.SwitchState(PlayerStates.JUMP);
            }
        }

        public override void OnStateExit()
        {
            player.currentSpeed = player.walkSpeed;
            if (player.anim != null)
                player.anim.speed = 1f;
        }
    }
}
