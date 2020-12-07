using System.Security.Cryptography;
using UnityEngine;
namespace GR.Enemy
{
    public class Asteroid : EnemyShip
    {
        Vector2 currentRotation = Vector2.up;
        float speed;
        float rotateSpeed;
        float childNumb;
        [SerializeField]
        Asteroid childPrefab;

        private void Start()
        {
            moveComponent.SetPosition(transform.position);
        }

        protected override void InternalFixedUpdate(float dt)
        {
            RotateDisplay();
        }

        protected override void Accelerate()
        {
            velocity = currentDirection.normalized * speed;
            velocity = Vector2.ClampMagnitude(velocity, speed);
            moveComponent.SetVelocity(velocity);
        }

        private void RotateDisplay()
        {
            float currentAngle = Core.Utilities.VectorToAngle(currentRotation);
            currentAngle += velocity.x < 0 ? rotateSpeed : -rotateSpeed;
            currentRotation = Core.Utilities.DegreeToVector2(currentAngle);
            displayComponent.SetDirection(currentRotation);
        }

        protected override void OnSetup()
        {
            speed = Random.Range(stats.GetStat("min_speed"), stats.GetStat("max_speed"));
            rotateSpeed = Random.Range(stats.GetStat("min_rotate_speed"), stats.GetStat("max_rotate_speed"));
            float scale = Random.Range(stats.GetStat("max_scale") * 1f, stats.GetStat("min_scale") * 1f);
            displayComponent.SetScale(scale);
            childNumb = stats.GetStat("number_of_child");
        }

        protected override void OnDead()
        {
            if (gameObject.activeInHierarchy)
            {
                if (childPrefab != null)
                {
                    for (int i = 0; i < childNumb; i++)
                    {
                        var child = ObjectPool.Spawn(childPrefab, GamePlayManager.enemyContainer);
                        child.Setup(CurrentPosition() + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                        child.SetDirection(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }

        protected override void OnTick()
        {

        }
    }
}