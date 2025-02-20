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
        Entity target;

        protected override void InternalUpdate(float dt)
        {
            preShootParticle.Simulate(dt, true, false);
        }

        protected override void Shoot(Bullet<EnemyShip> prefab)
        {
            if (target == null)
            {              
                target = ScanTarget();
            }
            if (target != null)
            {
                timer.After(timeBeforeShoot, () =>
                {
                    Vector2 dir = target.CurrentPosition() - owner.CurrentPosition();
                    var b = SpawnBullet(prefab);
                    b.Setup(owner, transform.position, dir);
                    target = null;
                    preShootParticle.Stop();
                    preShootParticle.gameObject.SetActive(false);
                });
            }
        }



        private Entity ScanTarget()
        {
            Collider2D col = Physics2D.OverlapCircle(owner.CurrentPosition(), searchRange, targetMask.value);
            if (col != null)
            {
                preShootParticle.Clear();
                preShootParticle.Simulate(0, false, true);
                preShootParticle.gameObject.SetActive(true);
                preShootParticle.Play();
                return col.GetComponentInParent<Entity>();
            }
            return null;
        }
    }
}