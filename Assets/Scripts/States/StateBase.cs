using UnityEngine;

namespace States
{
    public class StateBase
    {
        
        public virtual void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}] Enter");
        }

        
        public virtual void OnStateStay()
        {
            Debug.Log($"[{GetType().Name}] Stay");
        }

        
        public virtual void OnStateExit()
        {
            Debug.Log($"[{GetType().Name}] Exit");
        }
    }
}