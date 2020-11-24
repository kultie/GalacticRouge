using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVulnerable
{
    void DealDamage(Dictionary<string, object> args);
}
