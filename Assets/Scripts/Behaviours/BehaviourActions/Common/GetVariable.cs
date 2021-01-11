using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Common
{
    public class GetVariable : BehaviourActionBase
    {
        public string variableName;
        public override object Execute(EntityBehaviour entityBehaviour)
        {
            return entityBehaviour.GetVariable(variableName);
        }
    }
}