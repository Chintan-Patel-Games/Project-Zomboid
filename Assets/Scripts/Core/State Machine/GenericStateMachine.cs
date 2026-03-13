using System;
using System.Collections.Generic;

namespace ProjectZomboid.Core.StateMachine
{
    // Generic State Machine
    // Usage : Units and Game Loop
    public class GenericStateMachine<TOwner>
    {
        protected TOwner Owner;
        protected IState<TOwner> currentState;
        protected Enum currentStateKey;
        protected Dictionary<Enum, IState<TOwner>> States = new();

        public GenericStateMachine(TOwner Owner) => this.Owner = Owner;

        // Sets the owner : Only called by child class
        protected void SetOwner()
        {
            foreach (IState<TOwner> state in States.Values)
                state.Owner = Owner;
        }

        public void Update() => currentState?.UpdateState();

        // Changes state internally
        private void ChangeStateInternally(IState<TOwner> newState)
        {
            if (currentState == newState) return;

            currentState?.OnExitState();
            currentState = newState;
            currentState?.OnEnterState();
        }

        // Global method to change state
        public void ChangeState(Enum newState)
        {
            if (currentStateKey != null && currentStateKey.Equals(newState))
                return;

            currentStateKey = newState;
            ChangeStateInternally(States[newState]);
        }

        public Enum GetCurrentStateKey() => currentStateKey;
    }
}