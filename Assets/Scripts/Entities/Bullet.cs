﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet<T> : Entity where T : Entity
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int damage;
    protected T owner;

    public T GetOwner()
    {
        return owner;
    }

    private void Awake()
    {
        moveComponent.SetOnAtMapBound(OnMapBound);
    }
    protected void SetDirection(Vector2 dir)
    {
        currentDirection = dir;
        displayComponent.SetDirection(dir);
    }

    private void OnEnable()
    {
        Manager.UpdateManager.AddUpdate(OnUpdate);
    }

    private void OnDisable()
    {
        currentDirection = Vector2.zero;
        moveComponent.SetPosition(Vector2.zero);
        Manager.UpdateManager.RemoveUpdate(OnUpdate);
    }

    protected virtual void OnMapBound(Vector2 arg1, MapEdge arg2)
    {
        if (gameObject.activeInHierarchy)
        {
            ObjectPool.Recycle(gameObject);
        }
    }
    protected virtual void OnUpdate(float dt)
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<IVulnerable>();
        if (target == null)
        {
            target = collision.GetComponentInParent<IVulnerable>();
        }
        if (target != null && gameObject.activeInHierarchy)
        {
            OnCollide(target);
        }
    }
    protected abstract void OnCollide(IVulnerable target);
    protected abstract void Move();

    public virtual void Setup(T owner, Vector2 position, Vector2 direction)
    {
        this.owner = owner;
        moveComponent.SetPosition(position);
        SetDirection(direction);
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        OnSetup();
    }

    protected abstract void OnSetup();

    protected void Destroy()
    {
        if (gameObject.activeInHierarchy)
        {
            ObjectPool.Recycle(this);
        }
    }
}
