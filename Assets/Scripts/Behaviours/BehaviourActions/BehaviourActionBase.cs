using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GR.Behaviour
{
    [Serializable]
    public abstract class BehaviourActionBase
    {
        public virtual object Execute(EntityBehaviour entityBehaviour)
        {
            return null;
        }
        protected IEnumerable<System.Type> GetFilteredTypeList()
        {
            var q = typeof(BehaviourActionBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(BehaviourActionBase).IsAssignableFrom(x));

            return q;
        }
    }
}