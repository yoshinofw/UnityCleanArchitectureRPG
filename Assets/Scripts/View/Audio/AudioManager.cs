using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private SoundEmitter _soundEmitterPrefab;
        [SerializeField]
        private Transform _soundEmitterParent;
        [SerializeField]
        private int _initialCount;
        private ObjectPool<SoundEmitter> _soundEmitterPool;

        public void OnAudioEventTriggered(AudioEventTriggered e)
        {
            _soundEmitterPool.GetObject().Play(e.AudioClip, e.Volume, e.Pitch, e.SpatialBlend, e.Position);
        }

        private void Awake()
        {
            _soundEmitterPool = new ObjectPool<SoundEmitter>(_soundEmitterPrefab, _soundEmitterParent, _initialCount);
        }
    }
}