using Core.Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Components
{
    public class SpriteDisplay : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer renderer;

        Core.Animation.Animation anim;
        private void OnEnable()
        {
            Manager.UpdateManager.AddUpdate(UpdateDisplay);
        }

        private void OnDisable()
        {
            Manager.UpdateManager.RemoveUpdate(UpdateDisplay);
        }

        private void UpdateDisplay(float dt)
        {
            if (anim != null)
            {
                anim.Update(dt);
                renderer.sprite = anim.Frame();
            }
        }

        public void SetSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        public void Setup(Sprite displaySprite, AnimationData animData = null)
        {
            SetSprite(displaySprite);
            if (animData != null)
            {
                anim = new Core.Animation.Animation();
                anim.SetAnimation(animData);
            }
        }

        public void RequestAnimation(AnimationData data)
        {
            anim.SetAnimation(data);
        }

        public void SetDirection(Vector2 value)
        {
            renderer.transform.rotation = Quaternion.Euler(0, 0, Core.Utilities.VectorToAngle(value, 90));
        }
    }
}