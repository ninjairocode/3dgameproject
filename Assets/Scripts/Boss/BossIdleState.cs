using UnityEngine;
using States;

namespace Boss
{
    public class BossIdleState : StateBase
    {
        private readonly BossController boss;

        public BossIdleState(BossController boss)
        {
            this.boss = boss;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            boss.rb.linearVelocity = Vector3.zero;
        }

        public override void OnStateStay()
        {
            base.OnStateStay();

            if (boss.target == null)
                return;

            float distance = Vector3.Distance(boss.transform.position, boss.target.position);

            if (distance <= boss.rangeToDash)
            {
                boss.stateMachine.SwitchState(BossStates.ATTACK);
            }
        }
    }
}