using Core.EventDispatcher;
using GR.Enemy;
using JetBrains.Annotations;
using Kultie.TimerSystem;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public enum MapEdge
{
    None, Top, Bottom, Left, Right
}

public class GamePlayManager : MonoBehaviour
{
    static GamePlayManager Instance;
    [SerializeField]
    Level[] levels;
    [SerializeField]
    float minSpawn = 5;
    [SerializeField]
    float maxSpawn = 10;
    [SerializeField]
    float itemInterval = 16;
    [SerializeField]
    float bulletInterval = 30;

    int[] pointByLevel;
    int[] expByLevel;
    Timer timer;

    int currentLevel;
    int currentExp;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        timer = new Timer();
        GameStart();
    }

    public void GameStart()
    {
        UpdateManager.AddFixedUpdate(OnFixedUpdate);
        CreatePointByLevel();
        CreateExpByLevel();
        SpawnEnemy();
        timer.After(itemInterval, SpawnItem);
        timer.After(itemInterval, SpawnBullet);
    }



    private void OnLevelUp()
    {
        EventDispatcher.Dispatch("on_level_up", new Dictionary<string, object>() {
            {"current_level", currentLevel },
        });
    }

    void SpawnEnemy()
    {
        int point = 999;
        int level = levels.Length - 1;
        if (currentLevel < pointByLevel.Length)
        {
            point = pointByLevel[currentLevel];
            level = currentLevel;
        }

        EnemyShip[] enemies = levels[level].GetEnemies(point);
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyShip e = ObjectPool.Spawn(enemies[i]);
            e.Setup(RandomEnemyPosition());
        }
        timer.After(Random.Range(minSpawn, maxSpawn), SpawnEnemy);
    }

    Vector2 RandomEnemyPosition()
    {
        Vector2 pos = Vector2.zero;
        int random = Random.Range(0, 2) == 0 ? -1 : 1;
        float randomX = random * GameMap.mapBound.x - random * 0.2f;
        pos.x = randomX;
        pos.y = Random.Range(-GameMap.mapBound.y + 0.2f, GameMap.mapBound.y - 0.2f);
        return pos;
    }

    private void CreateExpByLevel()
    {
        expByLevel = new int[levels.Length];
        expByLevel[0] = 10;
        for (int i = 1; i < pointByLevel.Length; i++)
        {
            expByLevel[i] = expByLevel[i - 1] + (i * 5);
        }
    }

    void CreatePointByLevel()
    {
        pointByLevel = new int[levels.Length];
        pointByLevel[0] = 16;
        for (int i = 1; i < pointByLevel.Length - 3; i++)
        {
            pointByLevel[i] = pointByLevel[i - 1] + 8;
            pointByLevel[i + 1] = pointByLevel[i];
            pointByLevel[i + 2] = Mathf.FloorToInt(pointByLevel[i + 1] / 1.2f);
            pointByLevel[i + 3] = Mathf.CeilToInt(pointByLevel[i + 1] * 1.5f);
        }
    }

    public static void AddExp(int value)
    {
        if (Instance.currentLevel >= Instance.expByLevel.Length) return;
        Instance.currentExp += value;
        if (Instance.currentExp >= Instance.expByLevel[Instance.currentLevel])
        {
            Instance.currentExp = Instance.currentExp - Instance.expByLevel[Instance.currentLevel];
            Instance.currentLevel++;
            Instance.OnLevelUp();
        }
        if (Instance.currentLevel >= Instance.expByLevel.Length)
        {
            EventDispatcher.Dispatch("on_exp_change", new Dictionary<string, object>() {
                { "current_exp",  Instance.currentExp },
                { "current_require",  0}
            });
        }
        else
        {
            EventDispatcher.Dispatch("on_exp_change", new Dictionary<string, object>() {
                { "current_exp",  Instance.currentExp },
                { "current_require",  Instance.expByLevel[ Instance.currentLevel]}
            });
        }
    }

    private void SpawnItem()
    {
        timer.After(itemInterval, SpawnItem);
    }

    private void SpawnBullet()
    {
        timer.After(itemInterval, SpawnBullet);
    }

    void ProceduralLevelResolve()
    {

    }

    void OnFixedUpdate(float dt)
    {
        timer.Update(dt);
    }
}
