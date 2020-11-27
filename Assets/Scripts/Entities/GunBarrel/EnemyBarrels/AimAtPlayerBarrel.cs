using Components;
using Core.Animation;
using Kultie.TimerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public class AimAtPlayerBarrel : EnemyBarrel
    {
        [SerializeField]
        float searchRange;
        [SerializeField]
        LayerMask targetMask;
        [SerializeField]
        float timeBeforeShoot;
        [SerializeField]
        ParticleSystem preShootParticle;
        protected override void Shoot(Bullet<EnemyShip> prefab)
        {
            ScanTarget(prefab);
        }

        private void ScanTarget(Bullet<EnemyShip> prefab)
        {
            Collider2D col = Physics2D.OverlapCircle(owner.CurrentPosition(), searchRange, targetMask.value);
            if (col != null)
            {
                preShootParticle.gameObject.SetActive(true);
                timer.After(timeBeforeShoot, () =>
                {
                    preShootParticle.gameObject.SetActive(false);
                    var target = col.GetComponentInParent<Entity>();
                    Vector2 dir = target.CurrentPosition() - owner.CurrentPosition();
                    var b = SpawnBullet(prefab);
                    b.Setup(owner, transform.position, dir);
                });
            }
        }

        protected override void InternalUpdate(float dt)
        {
            preShootParticle.Simulate(dt, true, false);
        }

        protected override void InternalDisable()
        {
            preShootParticle.gameObject.SetActive(false);
        }
    }
}