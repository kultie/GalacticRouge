﻿using Core.EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public abstract class PlayerBarrel : GunBarrel<Ship>
    {
        private void Start()
        {
            EventDispatcher.Subscribe("on_player_tick", OnPlayerTick);
            EventDispatcher.Subscribe("on_player_update", OnPlayerUpdate);
        }

        private void OnPlayerUpdate(Dictionary<string, object> args)
        {
            float dt = (float)args["delta_time"];
            InternalUpdate(dt);
        }

        protected virtual void InternalUpdate(float dt) { }

        private void OnPlayerTick(Dictionary<string, object> args)
        {
            int totalTick = (int)args["total_tick"];
            if (totalTick % tickRate == 0)
            {
                Shoot(owner.CurrentBullet());
            }
        }
    }
}