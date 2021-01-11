using Sirenix.OdinInspector;
using UnityEngine;
namespace GR.Behaviour.Common
{
    public class VectorMath : BehaviourActionBase
    {
        public MathElementType elementType;

        [ShowIf("@this.elements != null && this.elements.Length > 1")]
        public MathOperator op;

        [ShowIf("elementType", MathElementType.RawValue)]
        public Vector2 value;

        [ShowIf("elementType", MathElementType.CalculatedValue)]
        [TypeFilter("GetFilteredTypeList")]
        public BehaviourActionBase[] elements;

        public override object Execute(EntityBehaviour entityBehaviour)
        {
            if (elementType == MathElementType.RawValue)
            {
                return value;
            }
            Vector2 result = (Vector2)elements[0].Execute(entityBehaviour);
            for (int i = 1; i < elements.Length; i++)
            {
                object value = elements[i].Execute(entityBehaviour);
                if (value is float)
                {
                    switch (op)
                    {
                        case MathOperator.Mult:
                            result *= (float)value;
                            break;
                        case MathOperator.Div:
                            result /= (float)value;
                            break;
                    }
                }
                else if (value is Vector2)
                {
                    switch (op)
                    {
                        case MathOperator.Plus:
                            result += (Vector2)value;
                            break;
                        case MathOperator.Minus:
                            result -= (Vector2)value;
                            break;
                        case MathOperator.Mult:
                            result *= (Vector2)value;
                            break;
                        case MathOperator.Div:
                            result /= (Vector2)value;
                            break;
                    }
                }
            }
            return result;
        }
    }
}