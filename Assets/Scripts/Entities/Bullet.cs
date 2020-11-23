using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    [SerializeField]
    float speed;
    //private void Awake()
    //{
    //    Setup(transform.position, Vector2.up);
    //}
    public void SetDirection(Vector2 dir)
    {
        currentDirection = dir;
        displayComponent.SetDirection(dir);
    }

    private void OnEnable()
    {
        moveComponent.SetOnAtMapBound(OnMapBound);
    }

    private void OnMapBound(Vector2 arg1, MapEdge arg2)
    {
        ObjectPool.Recycle(gameObject);
    }

    public void Setup(Vector2 position, Vector2 dir) {
        transform.position = position;
        SetDirection(dir);
        moveComponent.SetPosition(position);
        gameObject.SetActive(true);
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
}
