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
        protected Timer timer;
        private void Start()
        {
            EventDispatcher.Subscribe("on_enemy_tick_" + owner.GetInstanceID(), OnPlayerTick);
            EventDispatcher.Subscribe("on_enemy_update_" + owner.GetInstanceID(), OnPlayerUpdate);
            timer = new Timer();
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
                Shoot(owner.GetComponent<Shooter>().CurrentBullet());
            }
        }

        protected virtual void InternalDisable()
        {

        }


        protected abstract void InternalUpdate(float dt);
    }
}