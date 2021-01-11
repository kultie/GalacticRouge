using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Move
{
    public class MoveStraight : BehaviourActionBase
    {
        public override object Execute(EntityBehaviour entityBehaviour)
        {
            Vector2 velocity = entityBehaviour.entity.CurrentDirection() * (float)entityBehaviour.GetVariable("speed");
            entityBehaviour.entity.GetMoveComponent().SetVelocity(velocity);
            return base.Execute(entityBehaviour);
        }
    }
}