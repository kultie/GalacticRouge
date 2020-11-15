using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateStack
{
    public class StateStack<T> where T : StateContext
    {
        List<IState<T>> states;
        T context;
        public StateStack(T context)
        {
            this.context = context;
            states = new List<IState<T>>();
        }

        public IState<T> Pop()
        {
            var top = states[states.Count - 1];
            states.Remove(top);
            top.OnExit(context);
            return top;
        }

        public void Push(IState<T> state)
        {
            states.Add(state);
            state.OnEnter(context);
        }

        public void Update(float dt)
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                var _s = states[i];
                bool willContinue = _s.OnUpdate(dt, context);
                if (!willContinue)
                {
                    break;
                }
            }

            var top = states[states.Count - 1];
            top.OnInputHandle(context);
        }
    }
}