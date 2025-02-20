using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class NormalBarrel : PlayerBarrel
    {
        protected override void Shoot(Bullet<Ship> prefab)
        {
            var b = SpawnBullet(prefab);
            b.Setup(owner, transform.position, owner.CurrentDirection());
        }
    }
}