using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBullet : Bullet<Ship>
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
}
