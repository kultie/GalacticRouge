using System;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Tween
{
    public class SimpleTweener
    {
        private float timePassed;
        private TweenFunc func;
        private float start;
        private float finish;
        private float current;
        private float duration;
        float distance;
        private Action<float, float> onUpdate;
        private Action onStart;
        private Action onFinish;

        public SimpleTweener(float start, float finish, float duration, Action<float, float> onUpdate, EaseType easeType = EaseType.Linear, Action onStart = null, Action onFinish = null)
        {
            timePassed = 0;
            func = SimpleTween.tweenMap[easeType];
            this.start = start;
            this.finish = finish;
            this.duration = duration;
            distance = finish - start;
            current = start;
            this.onUpdate = onUpdate;
            this.onStart = onStart;
            this.onFinish = onFinish;
        }

        public void Update(float dt = 0)
        {
            if (dt == 0)
            {
                dt = Time.deltaTime;
            }
            if (timePassed == 0)
            {
                onStart?.Invoke();
            }
            timePassed = timePassed + dt;
            float fractionOfTime = timePassed / duration;
            current = start + func(fractionOfTime) * distance;
            if (timePassed >= duration)
            {
                current = start + distance;
                onUpdate?.Invoke(current, fractionOfTime);
                onFinish?.Invoke();
                return;
            }
            onUpdate?.Invoke(current, fractionOfTime);
        }

        public float Current()
        {
            return current;
        }
    }
}