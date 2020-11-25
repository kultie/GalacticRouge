using Components;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(SpriteDisplay))]
public class Entity : MonoBehaviour
{
    protected Vector2 currentDirection = Vector2.up;
    private Move _m;
    protected Move moveComponent
    {
        get
        {
            if (!_m)
            {
                _m = GetComponent<Move>();
            }
            return _m;
        }
    }

    private SpriteDisplay _d;
    protected SpriteDisplay displayComponent
    {
        get
        {
            if (!_d)
            {
                _d = GetComponent<SpriteDisplay>();
            }
            return _d;
        }
    }

    public Vector2 CurrentPosition() {
        return moveComponent.CurrentPosition();
    }
}
