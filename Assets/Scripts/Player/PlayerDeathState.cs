using States;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : StateBase
    {
        private PlayerController player;

        public PlayerDeathState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter()
        {
            player.anim.SetBool("Death", true);
            player.rb.linearVelocity = Vector3.zero;
        }
    }
}
