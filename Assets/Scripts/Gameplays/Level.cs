using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[Serializable]
public class Level
{
    [SerializeField]
    EnemyData[] enemyData;
    public Enemy[] GetEnemies(int point)
    {
        int currentPoint = point;
        List<Enemy> candicateEnemy = new List<Enemy>();
        for (int i = 0; i < enemyData.Length; i++)
        {
            for (int j = 0; j < enemyData[i].count; j++)
            {
                candicateEnemy.Add(enemyData[i].enemyPrefab);
            }

        }
        List<Enemy> enemies = new List<Enemy>();
        while (currentPoint > 0)
        {
            Enemy e = candicateEnemy[Random.Range(0, candicateEnemy.Count - 1)];
            enemies.Add(e);
            currentPoint -= e.spawnRequirePoint;
        }

        return enemies.ToArray();
    }
}
[Serializable]
public class EnemyData
{
    public Enemy enemyPrefab;
    public int count;
}
