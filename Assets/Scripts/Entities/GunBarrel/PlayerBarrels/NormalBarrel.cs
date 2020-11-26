﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class NormalBarrel : PlayerBarrel
    {
        protected override void Shoot()
        {
            var b = SpawnBullet();
            b.Setup(owner, transform.position, owner.CurrentDirection());
        }
    }
}