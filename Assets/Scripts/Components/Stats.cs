using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    StatDictionary stats;

    private StatDictionary modifiedStats;
    private Dictionary<string, bool> dirtyStats = new Dictionary<string, bool>();
    private Dictionary<string, StatsModiferContainer> statsModifier = new Dictionary<string, StatsModiferContainer>();
    private void Awake()
    {       
        modifiedStats = stats.Clone();
    }
    public int GetStat(string key)
    {
        if (dirtyStats.ContainsKey(key) && dirtyStats[key])
        {
            modifiedStats[key] = RecalculatedStats(key);
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
            if (statsModifier[container.key].tier > container.tier)
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
