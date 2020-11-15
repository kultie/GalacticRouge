using System;
using System.Collections;
using System.Collections.Generic;
using Core.StateMachine;
using UnityEngine;
namespace Core.StateMachine
{
    public interface IState<T> where T : StateContext
    {
        string key { get; }
        void OnUpdate(float dt, T context);

        void OnEnter(T context);

        void OnExit(T context);
    }
}