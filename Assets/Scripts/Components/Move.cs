using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Components
{
    [RequireComponent(typeof(Entity))]
    public class Move : MonoBehaviour
    {
        Vector2 currentPosition;
        Vector2 velocity;
        Action<Vector2, MapEdge> onAtMapBound;
        private void OnEnable()
        {
            Manager.UpdateManager.AddFixedUpdate(UpdatePosition);
        }
        private void OnDisable()
        {
            Manager.UpdateManager.RemoveFixedUpdate(UpdatePosition);
        }
        public void SetPosition(Vector2 value)
        {
            currentPosition = value;
        }

        public void SetVelocity(Vector2 value)
        {
            velocity = value;
        }

        private void UpdatePosition(float dt)
        {
            MapEdge edge = GamePlayManager.CheckMapEdge(currentPosition);
            if (edge != MapEdge.None)
            {
                currentPosition = FixMapPositionAtMapEdge(currentPosition, edge);
                onAtMapBound?.Invoke(velocity, edge);
            }
            currentPosition += velocity * dt;
            transform.position = currentPosition;

        }

        public void SetOnAtMapBound(Action<Vector2, MapEdge> func)
        {
            onAtMapBound = func;
        }

        public Vector2 RelectVelocity(Vector2 vel, Vector2 normal)
        {
            return Vector2.Reflect(vel, normal);
        }

        Vector2 FixMapPositionAtMapEdge(Vector2 value, MapEdge edge)
        {
            Vector2 result = value;
            switch (edge)
            {
                case MapEdge.Top:
                case MapEdge.Bottom:
                    value.y = value.y * GamePlayManager.mapBound.y / Mathf.Abs(value.y);
                    break;
                case MapEdge.Left:
                case MapEdge.Right:
                    value.x = value.x * GamePlayManager.mapBound.x / Mathf.Abs(value.x);
                    break;
            }

            return result;
        }
    }
}