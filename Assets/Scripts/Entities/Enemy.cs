using Core.EventDispatcher;
using MirzaBeig.ParticleSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public abstract class Enemy : Entity, IVulnerable
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
    protected Transform stateContainer;
    protected Vector2 velocity;
    public void Setup(Vector2 position)
    {
        moveComponent.SetPosition(position);
        moveComponent.SetOnAtMapBound(OnAtEdge);
    }

    protected virtual void OnAtEdge(Vector2 dir, MapEdge edge)
    {
        Destroy();
    }

    public void SetDirection(Vector2 value)
    {
        currentDirection = value;
    }

    private void OnEnable()
    {
        Manager.UpdateManager.AddFixedUpdate(OnFixedUpdate);
        _internalOnEnable();
    }

    protected abstract void _internalOnEnable();

    private void OnDisable()
    {
        Manager.UpdateManager.RemoveFixedUpdate(OnFixedUpdate);
    }

    protected virtual void OnFixedUpdate(float dt)
    {
        Accelerate();
    }

    protected virtual void Accelerate()
    {
        float speed = stats.GetStat("speed");
        velocity += currentDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
        displayComponent.SetDirection(velocity.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<IVulnerable>();
        if (target == null)
        {
            target = collision.GetComponentInParent<IVulnerable>();
        }
        if (target != null)
        {
            DamageResolve.Resolve(this, target, stats.GetStat("damage"));
        }
    }

    public void OnTakeDamage(Dictionary<string, object> args)
    {
        EventDispatcher.Dispatch("on_take_damage_" + GetInstanceID(), args);
        stats.ProcessHP(-stats.ProcessShield(-(int)args["damage"]));
        if (stats.CurrentHP() <= 0)
        {
            OnDead();
        }
    }

    protected void AddOnTakeDamageCallback(Caller func)
    {
        EventDispatcher.Subscribe("on_take_damage_" + GetInstanceID(), func);
    }

    protected void RemoveTakeDamageCallback(Caller func)
    {
        EventDispatcher.UnSubscribe("on_take_damage_" + GetInstanceID(), func);
    }

    protected abstract void OnDead();
    protected void Destroy()
    {
        if (gameObject.activeInHierarchy) {
            ObjectPool.Recycle(this);
        }
    }
}
