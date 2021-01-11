using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Move
{
    public class Accelerate : BehaviourActionBase
    {
        public float accelerateRate;

        public override object Execute(EntityBehaviour entityBehaviour)
        {
            Vector2 velocity = Vector2.zero;
            if (entityBehaviour.GetVariable("current_velocity") != null)
            {
                velocity = (Vector2)entityBehaviour.GetVariable("current_velocity");
            }

            velocity += accelerateRate * entityBehaviour.entity.CurrentDirection();
            try
            {
                velocity = Vector2.ClampMagnitude(velocity, (float)entityBehaviour.GetVariable("speed"));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.Log(velocity);
            }
            entityBehaviour.SetVariable("current_velocity", velocity);
            entityBehaviour.entity.GetMoveComponent().SetVelocity(velocity);
            return null;
        }
    }
}