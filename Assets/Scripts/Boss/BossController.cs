using Enemy;
using States;
using UnityEngine;

namespace Boss
{
    public class BossController : EnemyBase
    {
        public StateMachine<BossStates> stateMachine;

        [Header("Movement Settings")]
        public float dashSpeed = 30f;
        public float rangeToDash = 30f;
        public float dashCooldown = 4f;

        [Header("Attack Settings")]
        public float pushForce = 20f;
        public float hitDistance = 1f;

        [Header("Components")]
        //public Rigidbody rb;
        public Transform target;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            stateMachine = new StateMachine<BossStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossStates.IDLE, new BossIdleState(this));
            stateMachine.RegisterStates(BossStates.ATTACK, new BossAttackState(this));

            stateMachine.SwitchState(BossStates.IDLE);

            rb.isKinematic = false;
        }


        public override void Update()
        {
            
            base.Update();
            stateMachine.Update();
        }
    }

    public enum BossStates
    {
        IDLE,
        ATTACK
    }
}