using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Behaviour.Common
{
    public enum MathOperator
    {
        Mult,
        Div,
        Plus,
        Minus,
        Mod
    }

    public enum MathElementType
    {
        RawValue,
        CalculatedValue
    }
    public class Math : BehaviourActionBase
    {
        public MathElementType elementType;

        [ShowIf("@this.elements != null && this.elements.Length > 1")]
        public MathOperator op;

        [ShowIf("elementType", MathElementType.RawValue)]
        public float value;

        [ShowIf("elementType", MathElementType.CalculatedValue)]
        [TypeFilter("GetFilteredTypeList")]
        public BehaviourActionBase[] elements;

        public override object Execute(EntityBehaviour entityBehaviour)
        {
            if (elementType == MathElementType.RawValue)
            {
                return value;
            }
            float result = (float)elements[0].Execute(entityBehaviour);
            for (int i = 1; i < elements.Length; i++)
            {
                float value = (float)elements[i].Execute(entityBehaviour);
                switch (op)
                {
                    case MathOperator.Plus:
                        result += value;
                        break;
                    case MathOperator.Minus:
                        result -= value;
                        break;
                    case MathOperator.Mult:
                        result *= value;
                        break;
                    case MathOperator.Div:
                        result /= value;
                        break;
                    case MathOperator.Mod:
                        result %= value;
                        break;
                }
            }
            return result;
        }
    }
}