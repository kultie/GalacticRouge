using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using GR.Enemy;
[Serializable]
public class Level
{
    [SerializeField]
    EnemyData[] enemyData;
    public EnemyShip[] GetEnemies(int point)
    {
        int currentPoint = point;
        List<EnemyShip> candicateEnemy = new List<EnemyShip>();
        for (int i = 0; i < enemyData.Length; i++)
        {
            for (int j = 0; j < enemyData[i].count; j++)
            {
                candicateEnemy.Add(enemyData[i].enemyPrefab);
            }

        }
        List<EnemyShip> enemies = new List<EnemyShip>();
        while (currentPoint > 0)
        {
            EnemyShip e = candicateEnemy[Random.Range(0, candicateEnemy.Count - 1)];
            enemies.Add(e);
            currentPoint -= e.spawnRequirePoint;
        }

        return enemies.ToArray();
    }
}
[Serializable]
public class EnemyData
{
    public EnemyShip enemyPrefab;
    public int count;
}
