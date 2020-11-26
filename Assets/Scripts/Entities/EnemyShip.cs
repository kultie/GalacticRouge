﻿using Core.EventDispatcher;
using MirzaBeig.ParticleSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    [RequireComponent(typeof(Stats))]
    public abstract class EnemyShip : Entity, IVulnerable
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
        bool isDead;

        public int spawnRequirePoint;
        [SerializeField]
        int exp;

        int tickCounter;
        protected int totalTick;

        public void Setup(Vector2 position)
        {
            Manager.UpdateManager.AddFixedUpdate(OnFixedUpdate);
            moveComponent.SetPosition(position);
            moveComponent.SetOnAtMapBound(OnAtEdge);
            isDead = false;
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            Vector2 direction = Vector2.right;
            if (position.x > 0)
            {
                direction = Vector2.left;
            }
            SetDirection(direction);
            OnSetup();
        }

        protected abstract void OnSetup();

        protected virtual void OnAtEdge(Vector2 dir, MapEdge edge)
        {
            Destroy();
        }

        public void SetDirection(Vector2 value)
        {
            currentDirection = value;
        }

        protected virtual void OnFixedUpdate(float dt)
        {
            tickCounter++;
            if (tickCounter >= stats.GetStat("tick_rate"))
            {
                Tick();
                tickCounter = 0;
            }
            Accelerate();
        }

        protected virtual void Tick()
        {
            totalTick++;
            OnTick();
            EventDispatcher.Dispatch("on_enemy_tick_" + GetInstanceID(), new Dictionary<string, object> {
                { "total_tick", totalTick}
            });
        }

        protected abstract void OnTick();

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
            if (isDead) return;
            EventDispatcher.Dispatch("on_take_damage_" + GetInstanceID(), args);
            stats.ProcessHP(-stats.ProcessShield(-(int)args["damage"]));
            if (stats.CurrentHP() <= 0)
            {
                isDead = true;
                GamePlayManager.AddExp(exp);
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
            if (gameObject.activeInHierarchy)
            {
                ObjectPool.Recycle(this);
                Manager.UpdateManager.RemoveFixedUpdate(OnFixedUpdate);
            }
        }
    }
}