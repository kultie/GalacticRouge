using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Animation
{
    [Serializable]
    public class AnimationData
    {
        public Sprite[] frames;
        public float spf = 0.015f;
        public bool loop = false;
    }
}