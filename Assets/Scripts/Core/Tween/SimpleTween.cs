using System;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Tween
{
    public delegate float TweenFunc(float x);
    public enum EaseType
    {
        Linear,
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InBack,
        OutBack,
        InOutBack,
        InElastic,
        OutElastic,
        InOutElastic,
        OutBounce,
        InBounce,
        InOutBounce
    }
    public class SimpleTween
    {
        public static Dictionary<EaseType, TweenFunc> tweenMap = new Dictionary<EaseType, TweenFunc>()
        {
           {EaseType.Linear,            LINEAR },

           {EaseType.InSine,            IN_SINE },
           {EaseType.OutSine,           OUT_SINE },
           {EaseType.InOutSine,         IN_OUT_SINE },

           {EaseType.InQuad,            IN_QUAD },
           {EaseType.OutQuad,           OUT_QUAD },
           {EaseType.InOutQuad,         IN_OUT_QUAD },

           {EaseType.InCubic,           IN_CUBIC },
           {EaseType.OutCubic,          OUT_CUBIC },
           {EaseType.InOutCubic,        IN_OUT_CUBIC },

           {EaseType.InQuart,           IN_QUART },
           {EaseType.OutQuart,          OUT_QUART},
           {EaseType.InOutQuart,        IN_OUT_QUART },

           {EaseType.InQuint,           IN_QUINT },
           {EaseType.OutQuint,          OUT_QUINT},
           {EaseType.InOutQuint,        IN_OUT_QUINT},

           {EaseType.InExpo,            IN_EXPO },
           {EaseType.OutExpo,           OUT_EXPO },
           {EaseType.InOutExpo,         IN_OUT_EXPO },

           {EaseType.InCirc,            IN_CIRC },
           {EaseType.OutCirc,           OUT_CIRC},
           {EaseType.InOutCirc,         IN_OUT_CIRC },

           {EaseType.InBack,            IN_BACK },
           {EaseType.OutBack,           OUT_BACK },
           {EaseType.InOutBack,         IN_OUT_BACK },

           {EaseType.InElastic,         IN_ELASTIC },
           {EaseType.OutElastic,        OUT_ELASTIC },
           {EaseType.InOutElastic,      IN_OUT_ELASTIC },

           {EaseType.InBounce,          IN_BOUNCE },
           {EaseType.OutBounce,         OUT_BOUNCE },
           {EaseType.InOutBounce,       IN_OUT_BOUNCE },
        };

        #region stuff you don't need to know about only fix
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1;
        const float c4 = (2f * Mathf.PI) / 3f;
        const float c5 = (2f * Mathf.PI) / 4.5f;

        static float LINEAR(float x)
        {
            return x;
        }

        static float IN_SINE(float x)
        {
            return 1 - Mathf.Cos((x * Mathf.PI) / 2);
        }
        static float OUT_SINE(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2);
        }
        static float IN_OUT_SINE(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }

        static float IN_QUAD(float x)
        {
            return x * x;
        }
        static float OUT_QUAD(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }
        static float IN_OUT_QUAD(float x)
        {
            return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }

        static float IN_CUBIC(float x)
        {
            return x * x * x;
        }
        static float OUT_CUBIC(float x)
        {
            return 1 - Mathf.Pow(1 - x, 3);
        }
        static float IN_OUT_CUBIC(float x)
        {
            return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        static float IN_QUART(float x)
        {
            return x * x * x * x;
        }
        static float OUT_QUART(float x)
        {
            return 1 - Mathf.Pow(1 - x, 4);
        }
        static float IN_OUT_QUART(float x)
        {
            return x < 0.5 ? 8 * Mathf.Pow(x, 4) : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
        }

        static float IN_QUINT(float x)
        {
            return Mathf.Pow(x, 5);
        }
        static float OUT_QUINT(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }
        static float IN_OUT_QUINT(float x)
        {
            return x < 0.5 ? 16 * Mathf.Pow(x, 5) : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        }

        static float IN_EXPO(float x)
        {
            return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
        }
        static float OUT_EXPO(float x)
        {
            return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
        }
        static float IN_OUT_EXPO(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
        }

        static float IN_CIRC(float x)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
        }
        static float OUT_CIRC(float x)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
        }
        static float IN_OUT_CIRC(float x)
        {
            return x < 0.5
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        static float IN_BACK(float x)
        {
            return c3 * Mathf.Pow(x, 3) - c1 * x * x;
        }
        static float OUT_BACK(float x)
        {
            return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
        }
        static float IN_OUT_BACK(float x)
        {
            return x < 0.5
                ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }

        static float IN_ELASTIC(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }
        static float OUT_ELASTIC(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }
        static float IN_OUT_ELASTIC(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
                        : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
        }

        static float OUT_BOUNCE(float x)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (x < 1 / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2 / d1)
            {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            }
            else if (x < 2.5 / d1)
            {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            }
            else
            {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }
        static float IN_BOUNCE(float x)
        {
            return 1 - OUT_BOUNCE(1 - x);
        }
        static float IN_OUT_BOUNCE(float x)
        {
            return x < 0.5
                ? (1 - OUT_BOUNCE(1 - 2 * x)) / 2
                : (1 + OUT_BOUNCE(2 * x - 1)) / 2;
        }
        #endregion

        public static SimpleTweener Create(float start, float finish, float duration, Action<float, float> onUpdate, EaseType easeType = EaseType.Linear, Action onStart = null, Action onFinish = null)
        {
            return new SimpleTweener(start, finish, duration, onUpdate, easeType, onStart, onFinish);
        }
    }
}