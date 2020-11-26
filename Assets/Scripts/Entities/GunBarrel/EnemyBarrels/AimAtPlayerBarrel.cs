using Components;
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
        protected override void Shoot(Bullet<EnemyShip> prefab)
        {
            Collider2D col = Physics2D.OverlapCircle(owner.CurrentPosition(), searchRange, targetMask.value);
            if (col != null)
            {
                var target = col.GetComponentInParent<Move>();
                Vector2 dir = target.CurrentPosition() - owner.CurrentPosition();
                var b = SpawnBullet(prefab);
                b.Setup(owner, transform.position, dir);
            }

        }


    }
}