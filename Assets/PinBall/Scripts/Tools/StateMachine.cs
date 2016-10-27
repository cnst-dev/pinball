using System;
using System.Collections.Generic;

namespace ConstantineSpace.Tools
{
    public class StateMachine
    {
        private readonly Dictionary<Enum, State> _stateDictionary;
        private State _currentState;

        public Action OnStart;
        public Action OnStop;

        /// <summary>
        ///     Constructs a new StateMachine.
        /// </summary>
        public StateMachine()
        {
            _stateDictionary = new Dictionary<Enum, State>();
        }


        /// <summary>
        ///     Returns the StateHolder of the current state.
        /// </summary>
        public State CurrentState
        {
            get { return _currentState; }
        }

        /// <summary>
        ///     Adds a state, and the delegates that should run
        ///     when the state starts, stops,
        ///     and when the state machine is updated.
        ///     Any delegate can be null, and wont be executed.
        /// </summary>
        /// <param name="label">The name of the state to add.</param>
        public void AddState(Enum label)
        {
            _stateDictionary[label] = new State(label);
        }

        public override string ToString()
        {
            return CurrentState.ToString();
        }

        /// <summary>
        ///     Changes the state from the existing one to the state with the given StateHolder.
        ///     It is legal (and useful) to transition to the same state, in which case the
        ///     current state's onStop action is called, the onstart ation is called, and the
        ///     state keeps on updating as before. The behviour is exactly the same as switching to
        ///     a new state.
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(Enum newState)
        {
            if (_currentState != null && OnStop != null)
            {
                OnStop();
            }

            _currentState = _stateDictionary[newState];

            if (OnStart != null)
            {
                OnStart();
            }
        }

        public class State
        {
            public readonly Enum StateHolder;

            public State(Enum stateHolder)
            {
                StateHolder = stateHolder;
            }
        }
    }
}