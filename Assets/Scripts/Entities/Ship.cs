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
    [SerializeField]
    int tickRate;
    [SerializeField]
    Bullet currentBullet;
    [SerializeField]
    Transform gunPosition;
    Vector2 velocity;
    bool isBoosting;

    int tickCounter;

    private void Awake()
    {
        moveComponent.SetPosition(transform.position);
        moveComponent.SetOnAtMapBound(OnAtMapEdge);
    }

    private void OnEnable()
    {
        Manager.UpdateManager.AddFixedUpdate(OnFixedUpdate);
    }

    private void OnDisable()
    {
        Manager.UpdateManager.RemoveFixedUpdate(OnFixedUpdate);
    }

    private void OnFixedUpdate(float dt)
    {
        tickCounter++;
        if (tickCounter >= tickRate) {
            Tick();
            tickCounter = 0;
        }
    }

    protected virtual void Tick()
    {
        SpawnBullet();
    }

    private void OnAtMapEdge(Vector2 arg1, MapEdge arg2)
    {
        Vector2 norm = GamePlayManager.GetEdgeNormal(arg2);
        currentDirection = moveComponent.RelectVelocity(arg1.normalized, norm).normalized;
        float speed = isBoosting ? boostedSpeed : normalSpeed;
        velocity = currentDirection * speed;
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
        velocity += currentDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    public void Turn(bool turnRight)
    {
        float currentDir = Core.Utilities.VectorToAngle(currentDirection);
        currentDir += turnRight ? -turnRate : turnRate;
        currentDirection = Core.Utilities.DegreeToVector2(currentDir);
        currentDirection = currentDirection.normalized;
    }

    public void SetBoost(bool value)
    {
        isBoosting = value;
    }

    protected virtual void SpawnBullet() {
        Bullet b = ObjectPool.Spawn(currentBullet);
        b.transform.localScale = Vector3.one;
        b.Setup(gunPosition.position, currentDirection);
    }
}
