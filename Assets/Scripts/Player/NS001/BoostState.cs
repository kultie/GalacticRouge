using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : ShipStateBase<NS001Context>
{
    public override string key => "boost";

    public override void OnEnter(NS001Context context)
    {
        context.SetCurrentSpeed(context.stats.GetStat("boosted_speed"));
    }

    public override void OnExit(NS001Context context)
    {

    }

    public override void OnUpdate(float dt, NS001Context context)
    {

    }
}
