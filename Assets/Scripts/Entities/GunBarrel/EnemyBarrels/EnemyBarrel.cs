using Core.EventDispatcher;
using Kultie.TimerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace GR.Enemy
{
    public abstract class EnemyBarrel : GunBarrel<EnemyShip>
    {
        [SerializeField]
        EnemyBullet bullet;
        protected Timer timer;
        private void Start()
        {
            EventDispatcher.Subscribe("on_enemy_tick_" + owner.GetInstanceID(), OnPlayerTick);
            EventDispatcher.Subscribe("on_enemy_update_" + owner.GetInstanceID(), OnPlayerUpdate);
        }

        private void OnEnable()
        {
            timer = new Timer();
        }

        private void OnDestroy()
        {
            EventDispatcher.UnSubscribe("on_enemy_tick_" + owner.GetInstanceID(), OnPlayerTick);
            EventDispatcher.UnSubscribe("on_enemy_update_" + owner.GetInstanceID(), OnPlayerUpdate);
        }


        protected void OnPlayerUpdate(Dictionary<string, object> args)
        {
            float dt = (float)args["delta_time"];
            timer.Update(dt);
            InternalUpdate(dt);
        }

        private void OnPlayerTick(Dictionary<string, object> args)
        {
            int totalTick = (int)args["total_tick"];
            if (totalTick % tickRate == 0)
            {
                var b = bullet;
                if (b == null)
                {
                    b = owner.GetComponent<Shooter>().CurrentBullet();
                }
                Shoot(b);
            }
        }

        protected virtual void InternalDisable()
        {

        }


        protected abstract void InternalUpdate(float dt);
    }
}