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

        protected override void InternalUpdate(float dt)
        {

        }

        protected override void Shoot(Bullet<EnemyShip> prefab)
        {
            ScanTarget(prefab);
        }

        private void ScanTarget(Bullet<EnemyShip> prefab)
        {
            Collider2D col = Physics2D.OverlapCircle(owner.CurrentPosition(), searchRange, targetMask.value);
            if (col != null)
            {
                preShootParticle.Clear();
                preShootParticle.gameObject.SetActive(true);
                preShootParticle.Play();
                timer.After(timeBeforeShoot, () =>
                {
                    preShootParticle.Stop();
                    preShootParticle.gameObject.SetActive(false);
                    var target = col.GetComponentInParent<Entity>();
                    Vector2 dir = target.CurrentPosition() - owner.CurrentPosition();
                    var b = SpawnBullet(prefab);
                    b.Setup(owner, transform.position, dir);
                });
            }
        }
    }
}