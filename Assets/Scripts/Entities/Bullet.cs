using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    [SerializeField]
    float speed;

    private void Awake()
    {
        moveComponent.SetOnAtMapBound(OnMapBound);
    }
    public void SetDirection(Vector2 dir)
    {

        currentDirection = dir;
        displayComponent.SetDirection(dir);
    }

    private void OnDisable()
    {
        currentDirection = Vector2.zero;
        moveComponent.SetPosition(Vector2.zero);
    }

    private void OnMapBound(Vector2 arg1, MapEdge arg2)
    {
        ObjectPool.Recycle(gameObject);
    }

    public void Setup(Vector2 position, Vector2 dir)
    {
        gameObject.SetActive(true);
        transform.position = position;
        SetDirection(dir);
        moveComponent.SetPosition(position);
    }

    protected virtual void Update()
    {
        Accelerate();
    }

    private void Accelerate()
    {
        Vector2 velocity = currentDirection * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);
        moveComponent.SetVelocity(velocity);
        //displayComponent.SetDirection(velocity.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<IVulnerable>();
        if (target == null) {
            target = collision.GetComponentInParent<IVulnerable>();
        }
        if (target != null)
        {
            target.DealDamage(null);
        }
    }
}
