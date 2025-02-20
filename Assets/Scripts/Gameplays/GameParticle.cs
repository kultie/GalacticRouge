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
    public static GameParticle PlayParticle(GameParticle p, Vector2 position)
    {
        var _p = ObjectPool.Spawn(p, particleContainer, position);
        _p.transform.localScale = Vector3.one;
        _p.system.Clear();
        _p.system.Simulate(0, true, true);
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
