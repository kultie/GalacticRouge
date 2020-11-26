using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GR.Enemy
{
    public class Shooter : EnemyShip
    {
        [SerializeField]
        EnemyBullet bullet;
        [SerializeField]
        LayerMask targetMask;
        protected override void OnDead()
        {

        }

        protected override void OnSetup()
        {

        }

        protected override void OnTick()
        {

        }

        public EnemyBullet CurrentBullet()
        {
            return bullet;
        }
    }
}