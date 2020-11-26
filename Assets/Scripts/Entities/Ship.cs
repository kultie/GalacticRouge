using Components;
using Core.EventDispatcher;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Rigidbody2D))]
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
        PlayerBullet currentBullet;
        [SerializeField]
        protected Transform stateContainer;
        protected Vector2 velocity;

        int tickCounter;
        protected int totalTick;

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
            if (tickCounter % stats.GetStat("tick_rate") == 0)
            {
                Tick();
            }
        }

        protected virtual void Initialized()
        {

        }

        protected virtual void Tick()
        {
            totalTick++;
            EventDispatcher.Dispatch("on_player_tick", new Dictionary<string, object> {
                { "total_tick", totalTick}
            });
        }

        private void OnAtMapEdge(Vector2 arg1, MapEdge arg2)
        {
            Vector2 norm = GameMap.GetEdgeNormal(arg2);
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

        protected void ChangeBullet(PlayerBullet newBullet)
        {
            currentBullet = newBullet;
        }

        public PlayerBullet CurrentBullet()
        {
            return currentBullet;
        }

        public abstract void OnActiveSpecial(int index);

        public void OnTakeDamage(Dictionary<string, object> args)
        {
            EventDispatcher.Dispatch("on_player_take_damage", args);
            stats.ProcessHP(-stats.ProcessShield(-(int)args["damage"]));
            if (stats.CurrentHP() <= 0)
            {
                Debug.Log("Player is dead");
            }
        }

        protected void AddOnPlayerTakeDamageCallback(Caller func)
        {
            EventDispatcher.Subscribe("on_player_take_damage", func);
        }

        protected void RemoveOnPlayerTakeDamageCallback(Caller func)
        {
            EventDispatcher.UnSubscribe("on_player_take_damage", func);
        }
    }
}