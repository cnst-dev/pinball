using System;
using System.Collections.Generic;

namespace ConstantineSpace.Tools
{
    public class StateMachine<TState>
    {
        private class State
        {
            public readonly TState StateName;
            public readonly Action OnStart;
            public readonly Action OnStop;

            public State(TState stateName, Action onStart, Action onStop)
            {
                OnStart = onStart;
                OnStop = onStop;
                StateName = stateName;
            }
        }

        private readonly Dictionary<TState, State> _stateDictionary;
        private State _currentState;

        public TState CurrentState
        {
            get { return _currentState.StateName; }

            private set { SetState(value); }
        }

        public StateMachine()
        {
            _stateDictionary = new Dictionary<TState, State>();
        }

        /// <summary>
        ///     Adds the state.
        /// </summary>
        /// <param name="stateName">The name of the state to add.</param>
        /// <param name="onStart">OnStatr action.</param>
        /// <param name="onStop">OnStop action/</param>
        public void AddState(TState stateName, Action onStart, Action onStop)
        {
            _stateDictionary[stateName] = new State(stateName, onStart, onStop);
        }

        /// <summary>
        ///     Changes the state.
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(TState newState)
        {
            if (_currentState != null && _currentState.OnStop != null)
            {
                _currentState.OnStop();
            }

            _currentState = _stateDictionary[newState];

            if (_currentState.OnStart != null)
            {
                _currentState.OnStart();
            }
        }
    }
}