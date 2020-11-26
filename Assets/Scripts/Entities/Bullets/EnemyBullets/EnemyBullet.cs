using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public abstract class EnemyBullet : Bullet<EnemyShip>
    {
        protected override void Move()
        {
            Vector2 velocity = currentDirection * speed;
            velocity = Vector2.ClampMagnitude(velocity, speed);
            moveComponent.SetVelocity(velocity);
        }

        protected override void OnCollide(IVulnerable target)
        {
            DamageResolve.Resolve(this, target, damage);
            Destroy();
        }

        protected override void OnSetup()
        {

        }
    }
}