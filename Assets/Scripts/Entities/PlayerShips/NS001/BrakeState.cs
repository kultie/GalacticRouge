using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeState : ShipStateBase<NS001Context>
{
    public override string key => "brake";

    public override void OnEnter(NS001Context context)
    {
        context.SetCurrentSpeed(context.stats.GetStat("braking_speed"));
    }

    public override void OnExit(NS001Context context)
    {
      
    }

    public override void OnUpdate(float dt, NS001Context context)
    {
        
    }
}
