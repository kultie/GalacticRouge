using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBarrel<T> : MonoBehaviour where T : Entity
{
    [SerializeField]
    protected int tickRate = 1;
    private T _owner;
    protected T owner
    {
        get
        {
            if (!_owner)
            {
                _owner = GetComponentInParent<T>();
            }
            return _owner;
        }
    }

    protected Bullet<T> currentBullet;

    private int currentTickCount;
    private Transform _bulletContainer;
    private Transform bulletContainer
    {
        get
        {
            if (!_bulletContainer)
            {
                _bulletContainer = GameObject.Find("BulletContainer").transform;
            }
            return _bulletContainer;
        }
    }

    public void OnTick(Bullet<T> bullet)
    {
        currentBullet = bullet;
        currentTickCount++;
        if (currentTickCount % tickRate == 0)
        {
            Shoot();
        }
    }

    public void SetBullet(Bullet<T> bullet)
    {
        currentBullet = bullet;
    }

    protected abstract void Shoot();

    protected Bullet<T> SpawnBullet()
    {
        return ObjectPool.Spawn(currentBullet, bulletContainer);
    }
}
