using System;
using System.Collections;
using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.Item
{
    public class SceneItem : RecyclableObject
    {
        public event Action<SceneItem> TriggerEntered;
        public event Action<SceneItem> TriggerExited;
        public string ConfigId { get => _configId; set => _configId = value; }
        public int Count { get => _count; set => _count = value; }
        [SerializeField]
        private string _configId;
        [SerializeField]
        private int _count;
        [SerializeField]
        private ParticleSystem _particleSystem;

        public void OnPickuped()
        {
            StartCoroutine(ShowPickupedEffect());
        }

        public IEnumerator ShowPickupedEffect()
        {
            yield return new WaitForSeconds(0.4f);
            _particleSystem.Stop();
            while (_particleSystem.isPlaying)
            {
                yield return null;
            }
            Sleep();
        }

        protected override void Sleep()
        {
            TriggerEntered = null;
            TriggerExited = null;
            ConfigId = string.Empty;
            Count = 0;
            gameObject.SetActive(false);
            base.Sleep();
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                TriggerEntered?.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                TriggerExited?.Invoke(this);
            }
        }
    }
}