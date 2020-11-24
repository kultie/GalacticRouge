using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    Vector2 currentRotation = Vector2.up;
    int speed;
    int rotateSpeed;
    protected override void Initialized()
    {
        currentDirection = Vector2.right;
        moveComponent.SetPosition(transform.position);
    }

    protected override void OnFixedUpdate(float dt)
    {
        Accelerate();
        RotateDisplay();
    }

    protected override void Accelerate()
    {
        velocity += currentDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
    }

    private void RotateDisplay()
    {
        float currentAngle = Core.Utilities.VectorToAngle(currentRotation);
        currentAngle += velocity.x < 0 ? rotateSpeed : -rotateSpeed;
        currentRotation = Core.Utilities.DegreeToVector2(currentAngle);
        displayComponent.SetDirection(currentRotation);
    }

    protected override void _internalOnEnable()
    {
        speed = UnityEngine.Random.Range(stats.GetStat("min_speed"), stats.GetStat("max_speed"));
        rotateSpeed = UnityEngine.Random.Range(stats.GetStat("min_rotate_speed"), stats.GetStat("max_rotate_speed"));
    }

    protected override void OnDead()
    {
        Debug.Log("Asteroid is dead");
    }
}
