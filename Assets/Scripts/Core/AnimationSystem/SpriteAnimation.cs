using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        SpriteRenderer _rend;
        SpriteRenderer renderer
        {
            get
            {

                if (!_rend)
                {
                    _rend = GetComponent<SpriteRenderer>();
                }
                return _rend;
            }
        }

        Animation anims = new Animation();
        [SerializeField]
        AnimationData animData;

        public void PlayAnim(bool loop = true)
        {
            anims.SetAnimation(animData);
            gameObject.SetActive(true);
        }
        public void StopAnim()
        {
            gameObject.SetActive(false);
        }

        public void OnUpdate(float dt)
        {
            anims.Update(dt);
            renderer.sprite = anims.Frame();
        }
    }
}