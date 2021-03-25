using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
    public class StateMachine<TState> where TState : struct
    {
        private TState? _currentState;
        private readonly List<TState> _states;
        private readonly List<Transition<TState>> _transitions;

        public StateMachine()
        {
            _states = new List<TState>();
            _transitions = new List<Transition<TState>>();
        }

        public StateMachine(List<TState> states, List<Transition<TState>> transitions)
        {
            _states = states;
            _transitions = transitions;
        }

        public StateMachine<TState> WithState(TState state)
        {
            _states.Add(state);
            return this;
        }

        public StateMachine<TState> WithTransition(Transition<TState> transition)
        {
            _transitions.Add(transition);
            return this;
        }

        public StateMachine<TState> StartsWith(TState state)
        {
            EnsureStateExists(state);

            _currentState = state;
            return this;
        }

        public StateMachine<TState> SetState(TState state)
        {
            EnsureStateExists(state);

            if (_transitions.Any(x=>x.From.Equals(_currentState) && x.To.Equals(state)))
            {
                _currentState = state;
            }

            return this;
        }

        public TState CurrentState => _currentState ?? default(TState);

        private void EnsureStateExists(TState state)
        {
            if (!_states.Contains(state))
            {
                throw new ArgumentException("This state is not exists in StateMachine");
            }
        }
    }
}
