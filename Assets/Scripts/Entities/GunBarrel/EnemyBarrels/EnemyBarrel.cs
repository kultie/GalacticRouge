using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Enemy
{
    public abstract class EnemyBarrel : GunBarrel<EnemyShip>
    {
        public void Shoot(Vector2 direction)
        {
            var b = SpawnBullet();
            b.Setup(owner, transform.position, direction);
        }
    }
}