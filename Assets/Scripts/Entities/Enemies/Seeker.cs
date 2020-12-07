using Kultie.TimerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GR.Enemy
{
    public class Seeker : EnemyShip
    {
        [SerializeField]
        LayerMask searchMask;
        [SerializeField]
        ParticleSystem startMoveParticle;
        Entity player;
        Vector2 targetDirection;
        int currentTick;
        bool startMove;
        Timer timer;
        protected override void InternalFixedUpdate(float dt)
        {
            startMoveParticle.Simulate(dt, true, false);
            timer.Update(dt);
            if (!startMove)
            {
                if (player != null)
                {
                    currentTick++;
                    Vector2 targetPosition = player.CurrentPosition() + player.CurrentDirection() * stats.GetStat("see_ahead");
                    targetDirection = targetPosition - CurrentPosition();
                    SetDirection(targetDirection.normalized);
                }
            }

            if (player == null)
            {
                Collider2D col = Physics2D.OverlapCircle(CurrentPosition(), stats.GetStat("search_range"), searchMask.value);
                if (col != null)
                {
                    player = col.GetComponentInParent<Entity>();
                    timer.After(stats.GetStat("wait_time"), () =>
                    {
                        startMoveParticle.Clear();
                        startMoveParticle.Play();
                        startMoveParticle.Simulate(0, false, true);
                        startMoveParticle.gameObject.SetActive(true);
                        startMove = true;
                    });
                }
            }
        }

        protected override void Accelerate()
        {

            if (!startMove)
            {
                base.Accelerate();
                if (player != null)
                {
                    moveComponent.SetVelocity(Vector2.zero);
                }
            }
            else
            {

                moveComponent.SetVelocity(targetDirection.normalized * stats.GetStat("foward_speed"));
            }
        }

        protected override void OnDead()
        {

        }

        protected override void OnSetup()
        {
            currentTick = 0;
            player = null;
            startMove = false;
            timer = new Timer();
            startMoveParticle.Clear();
            startMoveParticle.gameObject.SetActive(false);
        }

        protected override void OnTick()
        {

        }
    }
}