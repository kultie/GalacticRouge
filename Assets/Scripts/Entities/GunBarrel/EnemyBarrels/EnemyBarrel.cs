using Core.EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public abstract class EnemyBarrel : GunBarrel<EnemyShip>
    {
        private void Start()
        {
            EventDispatcher.Subscribe("on_enemy_tick_" + owner.GetInstanceID(), OnPlayerTick);
        }

        private void OnPlayerTick(Dictionary<string, object> args)
        {
            int totalTick = (int)args["total_tick"];
            if (totalTick % tickRate == 0)
            {
                Shoot(owner.GetComponent<Shooter>().CurrentBullet());
            }
        }
    }
}