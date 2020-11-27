using Kultie.TimerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParticle : MonoBehaviour
{
    static Transform _pc;
    static Transform particleContainer
    {
        get
        {
            if (!_pc)
            {
                _pc = GameObject.Find("ParticleContainer").transform;
            }
            return _pc;
        }
    }

    ParticleSystem _sys;
    ParticleSystem system
    {
        get
        {
            if (!_sys)
            {
                _sys = GetComponent<ParticleSystem>();
            }
            return _sys;
        }
    }
    Timer timer;
    [SerializeField]
    bool autoDestroy;
    [SerializeField]
    float autoDestroyTimer = 2;
    private void OnEnable()
    {
        if (autoDestroy)
        {
            timer = new Timer();
            timer.After(autoDestroyTimer, Stop);
        }
        Manager.UpdateManager.AddFixedUpdate(OnFixedUpdate);
    }

    private void OnDisable()
    {
        timer = null;
        Manager.UpdateManager.RemoveFixedUpdate(OnFixedUpdate);
    }

    private void OnFixedUpdate(float dt)
    {
        if (timer != null)
        {
            timer.Update(dt);
        }
        system.Simulate(dt, true, false);
    }
    public static GameParticle PlayParticle(GameParticle p, Transform parent)
    {
        var _p = ObjectPool.Spawn(p, parent);
        _p.transform.localScale = Vector3.one;
        _p.system.Clear();
        _p.system.Play();
        return _p;
    }
    public static GameParticle PlayParticle(GameParticle p, Transform parent, Vector2 offSet)
    {
        Vector2 position = (Vector2)parent.position + offSet;
        var _p = ObjectPool.Spawn(p, parent, position);
        _p.transform.localScale = Vector3.one;
        _p.system.Clear();
        _p.system.Play();
        return _p;
    }

    public void Stop()
    {
        system.Clear();
        system.Stop();
        ObjectPool.Recycle(this);
    }
}
