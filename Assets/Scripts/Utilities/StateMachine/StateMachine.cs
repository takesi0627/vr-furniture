using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hekira.Utilities.StateMachine
{
    public class StateMachine<T>
    {
        private State<T> currentState;

        public StateMachine()
        {
            currentState = null;
        }

        public State<T> CurrentState
        {
            get { return currentState; }
        }

        internal void ChangeState(State<T> state, bool directly, params Object[] args)
        {
            if (currentState != null && !directly)
            {
                currentState.Exit();
            }
            currentState = state;
            currentState.ParameterList = args;
            currentState.Enter();
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }
    }
}