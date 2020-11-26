using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GR.Enemy
{
    public class Shooter : EnemyShip
    {
        [SerializeField]
        EnemyBarrel[] gunsBarrel;
        [SerializeField]
        EnemyBullet bullet;
        [SerializeField]
        LayerMask targetMask;
        protected override void OnDead()
        {

        }

        protected override void OnSetup()
        {
            for (int i = 0; i < gunsBarrel.Length; i++)
            {
                gunsBarrel[i].SetBullet(bullet);
            }
        }

        protected override void OnTick()
        {
            int searchRange = stats.GetStat("search_range");
            Collider2D col = Physics2D.OverlapCircle(CurrentPosition(), searchRange, targetMask.value);
            int tickRate = stats.GetStat("tick_rate");
            if (col != null)
            {
                var target = col.GetComponentInParent<Move>();
                Vector2 dir = target.CurrentPosition() - CurrentPosition();
                if (totalTick % tickRate == 0)
                {
                    for (int i = 0; i < gunsBarrel.Length; i++)
                    {
                        gunsBarrel[i].Shoot(dir);
                    }
                }
            }
        }
    }
}