using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Common
{
    public enum VariableType
    {
        Number,
        String,
        Vector
    }

    public class SetVariable : BehaviourActionBase
    {
        public string variableName;
        public VariableType variableType;

        public ValueType valueType;
        [ShowIf("@this.variableType == VariableType.String && this.valueType == ValueType.RawValue")]
        public string stringValue;
        [ShowIf("@this.variableType == VariableType.Number && this.valueType == ValueType.RawValue")]
        public float numberValue;
        [ShowIf("@this.variableType == VariableType.Vector && this.valueType == ValueType.RawValue")]
        public Vector2 vectorValue;

        [ShowIf("valueType", ValueType.CalculatedValue)]
        [TypeFilter("GetFilteredTypeList")]
        public BehaviourActionBase element;

        public override object Execute(EntityBehaviour entityBehaviour)
        {
            switch (valueType)
            {
                case ValueType.CalculatedValue:
                    entityBehaviour.SetVariable(variableName, element.Execute(entityBehaviour));
                    break;
                case ValueType.RawValue:
                    switch (variableType)
                    {
                        case VariableType.Number:
                            entityBehaviour.SetVariable(variableName, numberValue);
                            break;
                        case VariableType.String:
                            entityBehaviour.SetVariable(variableName, stringValue);
                            break;
                        case VariableType.Vector:
                            entityBehaviour.SetVariable(variableName, vectorValue);
                            break;
                    }
                    break;
            }
            return base.Execute(entityBehaviour);
        }
    }
}