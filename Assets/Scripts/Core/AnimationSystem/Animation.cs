using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Animation
{
    public class Animation
    {
        private int index;
        private float time;
        private AnimationData d;
        public void SetAnimation(AnimationData data)
        {
            d = data;
        }

        public void Update(float dt)
        {
            time += dt;
            if (time >= d.spf)
            {
                index += 1;
                time = 0;
                if (index > d.frames.Length - 1)
                {
                    if (d.loop)
                    {
                        index = 0;
                    }
                    else
                    {
                        index = d.frames.Length;
                    }
                }
            }
        }

        public Sprite Frame()
        {
            var i = index;
            if (i == d.frames.Length)
            {
                i = d.frames.Length - 1;
            }
            return d.frames[i];
        }

        public bool IsFinished()
        {
            return !d.loop && index == d.frames.Length;
        }
    }
}