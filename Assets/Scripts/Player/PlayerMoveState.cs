using States;
using UnityEngine;

namespace Player
{
    public class PlayerMoveState : StateBase
    {
        private PlayerController player;

        public PlayerMoveState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateStay()
        {
            float h = Input.GetAxisRaw("Horizontal"); // A / D → rotação
            float v = Input.GetAxisRaw("Vertical");   // W / S → movimento

           
            if (Mathf.Abs(h) > 0.1f)
            {
                
                float rotationSpeed = player.rotationSpeed; // defina no PlayerController
                player.transform.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime);
            }

            
            Vector3 vel = player.rb.linearVelocity;

            
            if (Mathf.Abs(v) > 0.1f)
            {
                Vector3 forward = player.transform.forward; // frente do player
                Vector3 move = forward * v * player.speed;

                vel.x = move.x;
                vel.z = move.z;
            }
            else
            {
                
                vel.x = 0f;
                vel.z = 0f;
            }

            player.rb.linearVelocity = vel;

           
            if (Mathf.Abs(v) < 0.1f && Mathf.Abs(h) < 0.1f)
            {
                player.stateMachine.SwitchState(PlayerStates.IDLE);
                return;
            }

            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.stateMachine.SwitchState(PlayerStates.JUMP);
            }
        }
    }
}