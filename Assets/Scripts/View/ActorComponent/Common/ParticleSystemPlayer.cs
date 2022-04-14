using System;
using System.Collections.Generic;
using UnityEngine;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class ParticleSystemPlayer : MonoBehaviour, IParticleSystemPlayer
    {
        [SerializeField]
        private NameParticleSystemPair[] _nameParticleSystemPairs;
        private Dictionary<string, ParticleSystem> _particleSystemPairsByName = new Dictionary<string, ParticleSystem>();

        public void Play(string name)
        {
            if (name == "Die")
            {
                _particleSystemPairsByName[name].transform.parent = null;
                Destroy(_particleSystemPairsByName[name].gameObject, 1);
            }
            _particleSystemPairsByName[name].Play();
        }

        private void Awake()
        {
            foreach (var pair in _nameParticleSystemPairs)
            {
                _particleSystemPairsByName.Add(pair.Name, pair.ParticleSystem);
            }
        }
    }

    [Serializable]
    public class NameParticleSystemPair
    {
        public string Name;
        public ParticleSystem ParticleSystem;
    }
}