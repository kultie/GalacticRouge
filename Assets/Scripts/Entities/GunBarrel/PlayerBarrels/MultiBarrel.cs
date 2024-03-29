﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class MultiBarrel : PlayerBarrel
    {
        [SerializeField]
        float directionOffset;
        [SerializeField]
        int bulletCount;
        protected override void Shoot(Bullet<Ship> prefab)
        {
            float middle = (bulletCount - 1) * 1f / 2;
            for (int i = 0; i < bulletCount; i++)
            {
                float currentOffset = i - middle;
                float currentAngle = Core.Utilities.VectorToAngle(owner.CurrentDirection());
                currentAngle += currentOffset * directionOffset;
                var b = SpawnBullet(prefab);
                b.Setup(owner, transform.position, Core.Utilities.DegreeToVector2(currentAngle));
            }
        }
    }
}