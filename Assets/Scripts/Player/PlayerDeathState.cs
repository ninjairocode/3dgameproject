using States;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : StateBase
    {
        private PlayerController player;
        private float timer;
        private float respawnDelay = 1.2f;

        public PlayerDeathState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            if (player == null) return;

            timer = 0f;

            
            if (player.anim != null)
            {
                player.anim.SetTrigger("Death");
                player.anim.SetBool("Run", false);
                player.anim.SetBool("isMoving", false);
            }

            
            if (player.rb != null)
            {
                player.rb.linearVelocity = Vector3.zero;
                player.rb.isKinematic = true;
            }

            
            Collider col = player.GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
        }

        public override void OnStateStay()
        {
            timer += Time.deltaTime;

            if (timer >= respawnDelay)
            {
                player.Respawn();
            }
        }

        public override void OnStateExit()
        {
            
            Collider col = player.GetComponent<Collider>();
            if (col != null)
                col.enabled = true;

            
            if (player.rb != null)
                player.rb.isKinematic = false;
        }
    }
}