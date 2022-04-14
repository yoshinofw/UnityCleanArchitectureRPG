using UnityEngine;

namespace UCARPG.View.Audio
{
    public class AudioEventTriggered
    {
        public AudioClip AudioClip { get; private set; }
        public float Volume { get; private set; }
        public float Pitch { get; private set; }
        public float SpatialBlend { get; private set; }
        public Vector3 Position { get; private set; }

        public AudioEventTriggered(AudioClip audioClip, float volume, float pitch, float spatialBlend, Vector3 position)
        {
            AudioClip = audioClip;
            Volume = volume;
            Pitch = pitch;
            SpatialBlend = spatialBlend;
            Position = position;
        }
    }
}