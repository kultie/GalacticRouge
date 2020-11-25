using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AcceleratedBullet : PlayerBullet
{
    [SerializeField]
    float acceleratedRate;
    Vector2 velocity;
    protected override void OnSetup()
    {
        velocity = Vector2.zero;
    }

    protected override void Move()
    {
        velocity += acceleratedRate * currentDirection;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
    }
}
