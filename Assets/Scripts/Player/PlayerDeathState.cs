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
            if (player == null) return;

            

            // Animação de morte (usar trigger é mais seguro que bool para uma única execução)
            if (player.anim != null)
            {
                player.anim.SetTrigger("Death");
                player.anim.SetBool("Run", false);
                player.anim.SetBool("isMoving", false);
            }

            // Zera velocidade física de forma segura
            if (player.rb != null)
            {
                // Se estiver usando linearVelocity (API customizada), mantenha-a; caso contrário, use velocity
                try
                {
                    player.rb.linearVelocity = Vector3.zero;
                }
                catch
                {
                    player.rb.linearVelocity = Vector3.zero;
                }

                // Opcional: tornar kinematic para evitar forças posteriores
                player.rb.isKinematic = true;
            }

            // Desativa colisores para evitar interações físicas indesejadas
            Collider col = player.GetComponent<Collider>();
            if (col != null) col.enabled = false;

            // Opcional: desabilitar o state machine para garantir que nenhum outro estado seja ativado
            // Se preferir manter a máquina ativa para efeitos, comente a linha abaixo
            player.stateMachine = null;
        }
    }
}
