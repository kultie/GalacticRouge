using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public class NormalBarrel : EnemyBarrel
    {
        protected override void Shoot()
        {
            var b = SpawnBullet();
            b.Setup(owner, transform.position, owner.CurrentDirection());
        }
    }
}