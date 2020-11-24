using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVulnerable
{
    void OnTakeDamage(Dictionary<string, object> args);
}
