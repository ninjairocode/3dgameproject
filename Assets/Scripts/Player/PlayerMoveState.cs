using Audio;
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

        public override void OnStateEnter()
        {
            if (player.anim != null)
                player.anim.SetBool("Run", true);
            
            var clip = SoundManager.Instance.sfxList.Find(s => s.id == "run").clip;
            SoundManager.Instance.PlayLoopSFX(clip);

            

            
        }

        public override void OnStateExit()
        {
            if (player.anim != null)
                player.anim.SetBool("Run", false);
            
            SoundManager.Instance.StopLoopSFX();

            

        }

        public override void OnStateStay()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            
            

            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.stateMachine.SwitchState(PlayerStates.SPRINT);
                return;
            }

            if (Mathf.Abs(h) > 0.1f)
            {
                player.transform.Rotate(Vector3.up, h * player.rotationSpeed * Time.deltaTime);
            }

            if (player.rb == null) return;

           
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

            if (Mathf.Abs(v) < 0.1f && Mathf.Abs(h) < 0.1f)
            {
                player.stateMachine.SwitchState(PlayerStates.IDLE);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.stateMachine.SwitchState(PlayerStates.JUMP);
                return;
            }
        }
    }
}