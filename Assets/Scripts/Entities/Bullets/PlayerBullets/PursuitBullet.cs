using Components;
using GR.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GR.Player
{
    public class PursuitBullet : PlayerBullet
    {
        [SerializeField]
        float searchRange;
        [SerializeField]
        float freeRange;
        [SerializeField]
        LayerMask searchMask;
        [SerializeField]
        float turnRate;

        Vector2 velocity;
        Entity currentTarget;

        protected override void OnSetup()
        {
            currentTarget = null;
        }

        protected override void OnUpdate(float dt)
        {
            Search();
            Move();
        }

        private void Search()
        {
            if (currentTarget == null)
            {
                var a = Physics2D.OverlapCircle(CurrentPosition(), searchRange, searchMask.value);
                if (a != null)
                {
                    currentTarget = a.GetComponentInParent<Entity>();
                }
            }
            else
            {
                Pursuit();
            }
        }

        private void Pursuit()
        {
            Vector2 targetDirection = currentTarget.CurrentPosition() - CurrentPosition();
            if (targetDirection.magnitude > freeRange || !currentTarget.gameObject.activeInHierarchy)
            {
                currentTarget = null;
            }
            else
            {
                currentDirection = Vector2.Lerp(currentDirection, targetDirection, turnRate);
                displayComponent.SetDirection(currentDirection);
            }
        }
    }
}