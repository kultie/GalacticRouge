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
            currentPosition += velocity * dt;
            transform.position = currentPosition;
        }
    }
}