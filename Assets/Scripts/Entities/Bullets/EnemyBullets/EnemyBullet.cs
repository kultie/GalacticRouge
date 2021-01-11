using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public abstract class EnemyBullet : Bullet<EnemyShip>
    {
        protected override void OnCollide(IVulnerable target)
        {
            DamageResolve.Resolve(this, target, damage);
            Destroy(target as Entity);
        }

        protected override void OnSetup()
        {

        }
    }
}