using Core.EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageResolve
{
    public static void Resolve(PlayerBullet bullet, IVulnerable target, int baseDamage = 0)
    {
        int damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "bullet", bullet },
                { "dealer", bullet.GetOwner()},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
        EventDispatcher.Dispatch("on_player_deal_damage", args);
    }

    public static void Resolve(Ship dealer, IVulnerable target, int baseDamage = 0)
    {
        int damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "dealer",dealer},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
        EventDispatcher.Dispatch("on_player_deal_damage", args);
    }

    public static void Resolve(Enemy dealer, IVulnerable target, int baseDamage = 0)
    {
        int damage = baseDamage;
        Dictionary<string, object> args = new Dictionary<string, object>() {
                { "dealer", dealer},
                { "damage", damage}
        };
        target.OnTakeDamage(args);
    }
}
