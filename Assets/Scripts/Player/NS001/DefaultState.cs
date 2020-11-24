using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ShipStateBase<NS001Context>
{
    public override string key => "default";

    public override void OnEnter(NS001Context context)
    {
        context.SetCurrentSpeed(context.stats.GetStat("speed"));
    }

    public override void OnExit(NS001Context context)
    {

    }

    public override void OnUpdate(float dt, NS001Context context)
    {

    }
}
