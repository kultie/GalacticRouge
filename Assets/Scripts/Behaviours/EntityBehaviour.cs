using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GR.Behaviour
{
    [CreateAssetMenu(fileName = "Behaviour", menuName = "Kultie/Create Behaviour", order = 1)]
    public class EntityBehaviour : SerializedScriptableObject
    {
        public Entity entity { private set; get; }

        [Header("On Setup")]
        [TypeFilter("GetFilteredTypeList")]
        [SerializeField]
        BehaviourActionBase[] onCreateBehaviour;
        [Header("On Update")]
        [TypeFilter("GetFilteredTypeList")]
        [SerializeField]
        BehaviourActionBase[] onUpdateBehaviour;
        [Header("On Destroy")]
        [TypeFilter("GetFilteredTypeList")]
        [SerializeField]
        BehaviourActionBase[] onDestroyBehaviour;

        private Dictionary<string, object> variables = new Dictionary<string, object>();
        public void Init(Entity e)
        {
            entity = e;
        }

        public object GetVariable(string key)
        {
            if (variables.ContainsKey(key))
            {
                return variables[key];
            }
            return null;
        }

        public void SetVariable(string key, object value)
        {
            variables[key] = value;
        }

        public void OnCreate()
        {
            for (int i = 0; i < onCreateBehaviour.Length; i++)
            {
                onCreateBehaviour[i].Execute(this);
            }
        }

        public void OnUpdate(float dt)
        {
            SetVariable("delta_time", dt);
            for (int i = 0; i < onUpdateBehaviour.Length; i++)
            {
                onUpdateBehaviour[i].Execute(this);
            }
        }

        public void OnEntityDestroy(Entity reason)
        {
            SetVariable("reason", reason);
            for (int i = 0; i < onDestroyBehaviour.Length; i++)
            {
                onDestroyBehaviour[i].Execute(this);
            }
            //variables.Clear();
        }

        public IEnumerable<System.Type> GetFilteredTypeList()
        {
            var q = typeof(BehaviourActionBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(BehaviourActionBase).IsAssignableFrom(x));

            return q;
        }
    }
}