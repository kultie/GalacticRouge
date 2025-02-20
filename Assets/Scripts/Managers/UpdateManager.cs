using Kultie.TimerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    public delegate void OnUpdate(float dt);
    public delegate void OnFixedUpdate(float dt);
    public class UpdateManager : SingletonManager<UpdateManager>
    {
        private OnUpdate onUpdate;
        private OnFixedUpdate onFixedUpdate;
        public static TickTimer timer;
        private void Start()
        {
            timer = new TickTimer();
        }

        public static void AddUpdate(OnUpdate updater)
        {
            Instance.onUpdate += updater;
        }

        public static void RemoveUpdate(OnUpdate updater)
        {
            Instance.onUpdate -= updater;
        }

        public static void AddFixedUpdate(OnFixedUpdate updater)
        {
            Instance.onFixedUpdate += updater;
        }

        public static void RemoveFixedUpdate(OnFixedUpdate updater)
        {
            Instance.onFixedUpdate -= updater;
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            onUpdate?.Invoke(dt);
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            timer.Update();
            onFixedUpdate?.Invoke(dt);
        }
    }
}