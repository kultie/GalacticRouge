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
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey(KeyCode.A))
        {
            ship.Turn(false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ship.Turn(true);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            ship.OnActiveSpecial(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ship.OnActiveSpecial(2);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            ship.OnActiveSpecial(0);
        }
#endif
        ship.Accelerate();
    }

    public void Turn(bool value) {
        ship.Turn(value);
    }

    public void ActiveSpecial(int index) {
        ship.OnActiveSpecial(index);
    }
}
