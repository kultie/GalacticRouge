using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public class NormalBarrel : EnemyBarrel
    {
        protected override void Shoot(Bullet<EnemyShip> prefab)
        {
            var b = SpawnBullet(prefab);
            b.Setup(owner, transform.position, owner.CurrentDirection());
        }
    }
}