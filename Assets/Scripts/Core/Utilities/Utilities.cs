using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public static class Utilities
    {
        public static float VectorToAngle(Vector2 value, float offSet = 0)
        {
            Vector2 vecNormal = value.normalized;
            return Mathf.Atan2(vecNormal.y, vecNormal.x) * Mathf.Rad2Deg - offSet;
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

    }
}