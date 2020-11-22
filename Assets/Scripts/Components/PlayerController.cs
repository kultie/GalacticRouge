using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Ship _s;
    protected Ship ship
    {
        get
        {
            if (!_s)
            {
                _s = GetComponent<Ship>();
            }
            return _s;
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ship.Turn(false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ship.Turn(true);
        }
        ship.Accelerate();

    }
}
