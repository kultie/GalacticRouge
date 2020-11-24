using Components;
using Core.EventDispatcher;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stats))]
public abstract class Ship : Entity, IVulnerable
{
    private Stats _st;
    protected Stats stats
    {
        get
        {
            if (!_st)
            {
                _st = GetComponent<Stats>();
            }
            return _st;
        }
    }

    [SerializeField]
    Bullet currentBullet;
    [SerializeField]
    Transform gunPosition;
    [SerializeField]
    protected Transform stateContainer;
    protected Vector2 velocity;

    int tickCounter;

    private void Awake()
    {
        moveComponent.SetPosition(transform.position);
        moveComponent.SetOnAtMapBound(OnAtMapEdge);
    }

    private void Start()
    {
        Initialized();
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
        if (tickCounter >= stats.GetStat("tick_rate"))
        {
            Tick();
            tickCounter = 0;
        }
    }

    protected virtual void Initialized()
    {

    }

    protected virtual void Tick()
    {
        SpawnBullet();
    }

    private void OnAtMapEdge(Vector2 arg1, MapEdge arg2)
    {
        Vector2 norm = GamePlayManager.GetEdgeNormal(arg2);
        currentDirection = moveComponent.RelectVelocity(arg1.normalized, norm).normalized;
        float speed = stats.GetStat("speed");
        velocity = currentDirection * speed;
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    public virtual void Accelerate()
    {
        float speed = stats.GetStat("speed");
        velocity += currentDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    public void Turn(bool turnRight)
    {
        float currentDir = Core.Utilities.VectorToAngle(currentDirection);
        int turnRate = stats.GetStat("turn_rate");
        currentDir += turnRight ? -turnRate : turnRate;
        currentDirection = Core.Utilities.DegreeToVector2(currentDir);
        currentDirection = currentDirection.normalized;
    }

    protected virtual void SpawnBullet()
    {
        Bullet b = ObjectPool.Spawn(currentBullet);
        b.transform.localScale = Vector3.one;
        b.Setup(this, gunPosition.position, currentDirection);
    }

    public abstract void OnActiveSpecial(int index);

    public void OnTakeDamage(Dictionary<string, object> args)
    {
        EventDispatcher.Dispatch("on_player_take_damage", args);
        stats.ProcessHP(-stats.ProcessShield(-(int)args["damage"]));
        if (stats.CurrentHP() <= 0) {
            Debug.Log("Player is dead");
        }
    }

    protected void AddOnPlayerTakeDamageCallback(Caller func) {
        EventDispatcher.Subscribe("on_player_take_damage", func);
    }

    protected void RemoveOnPlayerTakeDamageCallback(Caller func) {
        EventDispatcher.UnSubscribe("on_player_take_damage", func);
    }
}
