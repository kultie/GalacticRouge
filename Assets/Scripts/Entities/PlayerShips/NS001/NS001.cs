using Core.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player.NS
{
    public class NS001 : Ship
    {
        NS001Context context;
        StateMachine<NS001Context> mainStates;
        protected override void Initialized()
        {
            context = new NS001Context(stats);
            mainStates = new StateMachine<NS001Context>(context);
            IState<NS001Context>[] states = stateContainer.GetComponents<IState<NS001Context>>();
            for (int i = 0; i < states.Length; i++)
            {
                mainStates.AddState(states[i]);
            }
            mainStates.Change("default");
        }
        public override void Accelerate()
        {
            float speed = context.currentSpeed;
            velocity += currentDirection * speed;
            velocity = Vector2.ClampMagnitude(velocity, speed);
            moveComponent.SetVelocity(velocity);
            displayComponent.SetDirection(currentDirection);
        }

        public override void OnActiveSpecial(int index)
        {
            if (index == 0)
            {
                mainStates.Change("default");
            }
            else if (index == 1)
            {
                mainStates.Change("boost");
            }
            else if (index == 2)
            {
                mainStates.Change("brake");
            }
        }

    }

    public class NS001Context : ShipContext
    {
        public float currentSpeed { get; private set; }
        public Stats stats { get; private set; }
        public NS001Context(Stats stats)
        {
            this.stats = stats;
            currentSpeed = stats.GetStat("speed");
        }

        public void SetCurrentSpeed(float value)
        {
            currentSpeed = value;
        }
    }
}