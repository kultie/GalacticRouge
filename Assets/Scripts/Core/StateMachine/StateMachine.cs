using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    public class StateMachine<T> where T : StateContext
    {
        private Dictionary<string, IState<T>> states = new Dictionary<string, IState<T>>();
        public IState<T> currentState { private set; get; }
        private T context;
        public StateMachine(T context)
        {
            this.context = context;
        }

        public void AddState(IState<T> state)
        {
            states[state.key] = state;
        }

        public void Change(string key)
        {
            if (currentState != null)
            {
                currentState.OnExit(context);
            }
            if (states.ContainsKey(key))
            {
                currentState = states[key];
                currentState.OnEnter(context);
            }
        }

        public void Update(float dt)
        {
            if (currentState == null) return;
            currentState.OnUpdate(dt, context);
        }
    }
}