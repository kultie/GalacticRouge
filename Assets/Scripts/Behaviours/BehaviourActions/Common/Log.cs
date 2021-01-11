using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Common
{
    public enum ValueType
    {
        RawValue,
        CalculatedValue
    }
    public class Log : BehaviourActionBase
    {
        public ValueType valueType;
        [ShowIf("valueType", ValueType.RawValue)]
        public string value;

        [ShowIf("valueType", ValueType.CalculatedValue)]
        [TypeFilter("GetFilteredTypeList")]
        public BehaviourActionBase element;
        public override object Execute(EntityBehaviour entityBehaviour)
        {
            switch (valueType)
            {
                case ValueType.RawValue: Debug.Log(value); break;
                case ValueType.CalculatedValue: Debug.Log(element.Execute(entityBehaviour).ToString()); break;
            }
            return null;
        }
    }
}