using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.Audio
{
    public class SoundEmitter : RecyclableObject
    {
        [SerializeField]
        private AudioSource _audioSource;
        private Transform _parent;

        public void Play(AudioClip audioClip, float volume, float pitch, float spatialBlend, Vector3 position)
        {
            gameObject.SetActive(true);
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            _audioSource.spatialBlend = spatialBlend;
            transform.position = position;
            _audioSource.Play();
        }

        protected override void Sleep()
        {
            gameObject.SetActive(false);
            base.Sleep();
        }

        private void Awake()
        {
            _parent = transform.parent;
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }
            if (_parent == null)
            {
                Destroy(gameObject);
                return;
            }
            transform.parent = _parent;
            Sleep();
        }
    }
}