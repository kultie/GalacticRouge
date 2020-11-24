using Core.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipStateBase<T> : MonoBehaviour, IState<T> where T : ShipContext
{
    public abstract string key { get; }

    public abstract void OnEnter(T context);

    public abstract void OnExit(T context);

    public abstract void OnUpdate(float dt, T context);
}
