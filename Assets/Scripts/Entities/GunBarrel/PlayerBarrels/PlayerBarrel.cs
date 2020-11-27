using Core.EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public abstract class PlayerBarrel : GunBarrel<Ship>
    {
        [SerializeField]
        PlayerBullet bullet;
        private void Start()
        {
            EventDispatcher.Subscribe("on_player_tick", OnPlayerTick);
            EventDispatcher.Subscribe("on_player_update", OnPlayerUpdate);
        }

        private void OnDestroy()
        {
            EventDispatcher.UnSubscribe("on_player_tick", OnPlayerTick);
            EventDispatcher.UnSubscribe("on_player_update", OnPlayerUpdate);
        }

        private void OnPlayerUpdate(Dictionary<string, object> args)
        {
            float dt = (float)args["delta_time"];
            InternalUpdate(dt);
        }

        public void SetBullet(PlayerBullet bullet)
        {
            this.bullet = bullet;
        }

        protected virtual void InternalUpdate(float dt) { }

        private void OnPlayerTick(Dictionary<string, object> args)
        {
            int totalTick = (int)args["total_tick"];
            if (totalTick % tickRate == 0)
            {
                var b = bullet;
                if (b == null)
                {
                    b = owner.CurrentBullet();
                }
                Shoot(b);
            }
        }
    }
}