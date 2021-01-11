using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class PlayerBullet : Bullet<Ship>
    {
        protected override void OnCollide(IVulnerable target)
        {
            DamageResolve.Resolve(this, target, damage);
            Destroy(target as Entity);
        }
    }
}