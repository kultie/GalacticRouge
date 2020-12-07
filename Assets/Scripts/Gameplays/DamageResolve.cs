using Core.EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageResolve
{
    public static void Resolve(GR.Player.PlayerBullet bullet, IVulnerable target, float baseDamage = 0)
    {
        float damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "bullet", bullet },
                { "dealer", bullet.GetOwner()},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
        EventDispatcher.Dispatch("on_player_deal_damage", args);
    }

    public static void Resolve(GR.Enemy.EnemyBullet bullet, IVulnerable target, float baseDamage = 0)
    {
        float damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "bullet", bullet },
                { "dealer", bullet.GetOwner()},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
        EventDispatcher.Dispatch("on_player_deal_damage", args);
    }

    public static void Resolve(GR.Player.Ship dealer, IVulnerable target, float baseDamage = 0)
    {
        float damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "dealer",dealer},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
        EventDispatcher.Dispatch("on_player_deal_damage", args);
    }

    public static void Resolve(GR.Enemy.EnemyShip dealer, IVulnerable target, float baseDamage = 0)
    {
        float damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "dealer", dealer},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
    }
}
