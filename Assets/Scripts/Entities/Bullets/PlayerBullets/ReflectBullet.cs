using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBullet : PlayerBullet
{
    [SerializeField]
    int bounceCount;
    int currentBounceCount;
    protected override void OnSetup()
    {
        currentBounceCount = bounceCount;
    }

    protected override void OnMapBound(Vector2 arg1, MapEdge arg2)
    {
        if (currentBounceCount > 0)
        {
            Vector2 norm = GameMap.GetEdgeNormal(arg2);
            currentDirection = moveComponent.RelectVelocity(arg1.normalized, norm).normalized;
            moveComponent.SetVelocity(currentDirection * speed);
            displayComponent.SetDirection(currentDirection);
            currentBounceCount--;
        }
        else {
            Destroy();
        }
    }
}
