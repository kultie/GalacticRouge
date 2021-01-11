using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class AcceleratedBullet : PlayerBullet
    {
        [SerializeField]
        float acceleratedRate;
        Vector2 velocity;
        protected override void OnSetup()
        {
            velocity = Vector2.zero;
        }
    }
}