using UnityEngine;
using UnityEngine.UI;
using UCARPG.Utilities;

namespace UCARPG.View.UI
{
    public class DamageText : RecyclableObject
    {
        [SerializeField]
        private Animation _animation;
        [SerializeField]
        private Text _text;

        public void Play(string value, Vector3 position)
        {
            gameObject.SetActive(true);
            _text.text = value;
            transform.position = position;
            _animation.Play();
        }

        protected override void Sleep()
        {
            gameObject.SetActive(false);
            base.Sleep();
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (!_animation.isPlaying)
            {
                Sleep();
            }
        }
    }
}