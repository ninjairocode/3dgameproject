using System.Collections.Generic;

namespace States
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState => _currentState;

        
        public StateMachine()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void Init()
        {
            
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }

        public void SwitchState(T state)
        {
            if (_currentState != null)
                _currentState.OnStateExit();

            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            _currentState?.OnStateStay();
        }
    }
}