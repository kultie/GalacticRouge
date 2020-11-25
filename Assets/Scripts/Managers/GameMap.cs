using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMap {
    public static Vector2 mapBound = new Vector2(9, 5);

    public static MapEdge CheckMapEdge(Vector2 position, Vector2 offset)
    {
        if (Mathf.Abs(position.x) > mapBound.x + offset.x)
        {
            return position.x > 0 ? MapEdge.Right : MapEdge.Left;
        }

        if (Mathf.Abs(position.y) > mapBound.y + offset.y)
        {
            return position.y > 0 ? MapEdge.Top : MapEdge.Bottom;
        }
        return MapEdge.None;
    }

    public static Vector2 GetEdgeNormal(MapEdge arg)
    {
        switch (arg)
        {
            case MapEdge.Bottom:
                return Vector2.up;
            case MapEdge.Left:
                return Vector2.right;
            case MapEdge.Right:
                return Vector2.left;
            case MapEdge.Top:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }
}
