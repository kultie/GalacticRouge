using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEditor;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    StatDictionary stats;
    [SerializeField]
    BasicStats basicStats;
    private int currentHP;
    private int currentShield;

    private StatDictionary modifiedStats;
    private Dictionary<string, bool> dirtyStats = new Dictionary<string, bool>();
    private Dictionary<string, StatsModiferContainer> statsModifier = new Dictionary<string, StatsModiferContainer>();
    private void Awake()
    {
        stats["max_hp"] = basicStats.maxHP;
        stats["max_shield"] = basicStats.maxShield;
        stats["tick_rate"] = basicStats.tickRate;
        modifiedStats = stats.Clone();
        currentHP = GetStat("max_hp");
        currentShield = GetStat("max_shield");
    }

    public void ApplyModifier(StatsModiferContainer[] containers)
    {
        currentHP = GetStat("max_hp");
        currentShield = GetStat("max_shield");
        for (int i = 0; i < containers.Length; i++)
        {
            AddModifier(containers[i]);
        }
    }
    public int GetStat(string key)
    {
        if (!modifiedStats.ContainsKey(key)) return 0;
        if (dirtyStats.ContainsKey(key) && dirtyStats[key])
        {
            int oldValue = modifiedStats["max_hp"];

            modifiedStats[key] = RecalculatedStats(key);
            UpdateStat(key, oldValue, modifiedStats[key]);
            dirtyStats[key] = false;
        }
        return modifiedStats[key];
    }

    private int RecalculatedStats(string key)
    {
        int rawStats = stats[key];
        float totalPercent = 0;
        int totalFlat = 0;
        foreach (var kv in statsModifier)
        {
            var mod = kv.Value;
            var mods = mod.modiferValues;
            totalPercent += mods[key].percent;
            totalFlat += mods[key].flat;
        }
        return Mathf.RoundToInt((1 + totalPercent) * (rawStats + totalFlat));
    }

    private void AddModifier(StatsModiferContainer container)
    {
        if (statsModifier.ContainsKey(container.key))
        {
            if (statsModifier[container.key].tier >= container.tier)
            {
                return;
            }
        }
        statsModifier[container.key] = container;
        foreach (var kv in container.modiferValues)
        {
            dirtyStats[kv.Key] = true;
        }
    }

    private void RemoveModifier(string key)
    {
        var mod = statsModifier[key];
        statsModifier.Remove(key);
        foreach (var kv in mod.modiferValues)
        {
            dirtyStats[kv.Key] = true;
        }
    }

    public int CurrentHP()
    {
        return currentHP;
    }

    public int CurrentShield()
    {
        return currentShield;
    }

    public void ProcessHP(int value)
    {
        currentHP += value;
        currentHP = Mathf.Clamp(currentHP, 0, GetStat("max_hp"));
    }

    public int ProcessShield(int value)
    {
        int leftOver = 0;
        if (value < 0)
        {
            leftOver = Mathf.Abs(value) - currentShield;
            leftOver = Mathf.Clamp(leftOver, 0, leftOver);
        }
        currentShield += value;
        currentShield = Mathf.Clamp(currentShield, 0, GetStat("max_shield"));
        return leftOver;
    }

    private void UpdateStat(string key, int oldValue, int newValue)
    {
        float percentChange = newValue * 1f / oldValue;
        if (key == "max_hp")
        {
            if (percentChange > 1)
            {
                currentHP = Mathf.RoundToInt(percentChange * currentHP);
            }
            ProcessHP(0);
        }
        else if (key == "max_shield")
        {
            if (percentChange > 1)
            {
                currentShield = Mathf.RoundToInt(percentChange * currentShield);
            }
            ProcessShield(0);
        }
    }
}
[Serializable]
public class StatDictionary : SerializableDictionary<string, int>
{
    public StatDictionary Clone()
    {
        StatDictionary result = new StatDictionary();
        foreach (var kv in this)
        {
            result[kv.Key] = kv.Value;
        }
        return result;
    }
}

public class StatsModiferContainer
{
    public string key;
    public int tier;
    public Dictionary<string, StatsModifierValue> modiferValues;
}

public class StatsModifierValue
{
    public float percent;
    public int flat;
}
[Serializable]
public class BasicStats
{
    public int maxHP;
    public int maxShield;
    public int tickRate;
}
