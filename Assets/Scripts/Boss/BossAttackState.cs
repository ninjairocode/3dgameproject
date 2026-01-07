using UnityEngine;
using States;
using System.Collections;
using Camera;
using Effects;

namespace Boss
{
    public class BossAttackState : StateBase
    {
        private readonly BossController boss;

        private bool isDashing;
        private bool cooldownStarted;

        
        private float lastDistanceToTarget = Mathf.Infinity;

        public BossAttackState(BossController boss)
        {
            this.boss = boss;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            if (boss.target == null)
            {
                boss.stateMachine.SwitchState(BossStates.IDLE);
                return;
            }

            isDashing = true;
            cooldownStarted = false;

            
            lastDistanceToTarget = Vector3.Distance(boss.transform.position, boss.target.position);
        }

        public override void OnStateStay()
        {
            base.OnStateStay();

            if (!isDashing)
                return;

            DashTowardsTarget();
        }

        private void DashTowardsTarget()
        {
            if (boss.target == null)
            {
                StopDashAndGoIdle();
                return;
            }

            Vector3 toTarget = boss.target.position - boss.transform.position;
            float distanceToTarget = toTarget.magnitude;

            
            if (distanceToTarget <= boss.hitDistance)
            {
                OnHitTarget();
                return;
            }

            Vector3 direction = toTarget.normalized;
            boss.rb.linearVelocity = direction * boss.dashSpeed;

            
            if (distanceToTarget > lastDistanceToTarget + 0.05f)
            {
                
                OnHitTarget();
                return;
            }

            lastDistanceToTarget = distanceToTarget;
        }

        private void OnHitTarget()
        {
            boss.rb.linearVelocity = Vector3.zero;
            isDashing = false;

            PushTarget();
            CameraShakeController.Instance.Shake();
            EffectsManager.Instance.DamageFlash();

            if (!cooldownStarted)
                boss.StartCoroutine(CooldownRoutine());
        }

        private void PushTarget()
        {
            if (boss.target == null)
                return;

            Rigidbody targetRb = boss.target.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Vector3 pushDir = (boss.target.position - boss.transform.position).normalized;
                targetRb.AddForce(pushDir * boss.pushForce, ForceMode.Impulse);
            }
        }

        private IEnumerator CooldownRoutine()
        {
            cooldownStarted = true;

            yield return new WaitForSeconds(boss.dashCooldown);

            boss.stateMachine.SwitchState(BossStates.IDLE);
        }

        private void StopDashAndGoIdle()
        {
            isDashing = false;
            boss.rb.linearVelocity = Vector3.zero;
            boss.stateMachine.SwitchState(BossStates.IDLE);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.rb.linearVelocity = Vector3.zero;
        }
    }
}