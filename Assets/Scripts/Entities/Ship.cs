using Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Entity
{
    [SerializeField]
    float normalSpeed;
    [SerializeField]
    float boostedSpeed;
    [SerializeField]
    float turnRate;
    Vector2 moveDirection = Vector2.up;
    Vector2 velocity;
    bool isBoosting;

    private Move _m;
    protected Move moveComponent
    {
        get
        {
            if (!_m)
            {
                _m = GetComponent<Move>();
            }
            return _m;
        }
    }

    private SpriteDisplay _d;
    protected SpriteDisplay displayComponent
    {
        get
        {
            if (!_d)
            {
                _d = GetComponent<SpriteDisplay>();
            }
            return _d;
        }
    }

    private void Awake()
    {
        moveComponent.SetPosition(transform.position);
        moveComponent.SetOnAtMapBound(OnAtMapEdge);
    }

    private void OnAtMapEdge(Vector2 arg1, MapEdge arg2)
    {
        Vector2 norm = GamePlayManager.GetEdgeNormal(arg2);
        moveDirection = moveComponent.RelectVelocity(arg1.normalized, norm).normalized;
        float speed = isBoosting ? boostedSpeed : normalSpeed;
        velocity = moveDirection * speed;
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    public void Setup(JObject data)
    {
        normalSpeed = data["normal_speed"].Value<float>();
        boostedSpeed = data["boosted_speed"].Value<float>();
        turnRate = data["turn_rate"].Value<float>();
    }

    public void Accelerate()
    {
        float speed = isBoosting ? boostedSpeed : normalSpeed;
        velocity += moveDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    public void Turn(bool turnRight)
    {
        float currentDir = Core.Utilities.VectorToAngle(moveDirection);
        currentDir += turnRight ? -turnRate : turnRate;
        moveDirection = Core.Utilities.DegreeToVector2(currentDir);
        moveDirection = moveDirection.normalized;
    }

    public void SetBoost(bool value)
    {
        isBoosting = value;
    }
}
