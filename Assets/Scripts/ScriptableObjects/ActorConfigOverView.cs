using System;
using System.Collections.Generic;
using UnityEngine;

namespace UCARPG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ActorConfigOverView", menuName = "UCARPG/ActorConfigOverView", order = 0)]
    public class ActorConfigOverView : ScriptableObject, ISerializationCallbackReceiver
    {
        public ActorConfig this[string id] { get => _actorConfigsById[id]; }
        private Dictionary<string, ActorConfig> _actorConfigsById;
        [SerializeField]
        private ActorConfig[] _actorConfigs;

        public void OnAfterDeserialize()
        {
            _actorConfigsById = new Dictionary<string, ActorConfig>();
            foreach (var actorConfig in _actorConfigs)
            {
                _actorConfigsById[actorConfig.Id] = actorConfig;
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }

    [Serializable]
    public class ActorConfig
    {
        public string Id { get => _id; }
        public GameObject Prefab { get => _prefab; }
        public string DefaultWeaponConfigId { get => _defaultWeaponConfigId; }
        public StatConfigOverView StatConfigOverView { get => _statConfigOverView; }
        [SerializeField]
        private string _id;
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private string _defaultWeaponConfigId;
        [SerializeField]
        private StatConfigOverView _statConfigOverView;
    }

    [Serializable]
    public class StatConfigOverView : ISerializationCallbackReceiver
    {
        public StatConfig this[string type] { get => _statConfigsById[type]; }
        public StatConfig[] All { get => _statConfigs; }
        private Dictionary<string, StatConfig> _statConfigsById;
        [SerializeField]
        private StatConfig[] _statConfigs;

        public void OnAfterDeserialize()
        {
            _statConfigsById = new Dictionary<string, StatConfig>();
            foreach (var statConfig in _statConfigs)
            {
                _statConfigsById[statConfig.Type] = statConfig;
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }

    [Serializable]
    public class StatConfig
    {
        public string Type { get => _type; }
        public float MaxValue { get => _maxValue; }
        public GameObject Prefab { get => _prefab; }
        [SerializeField]
        private string _type;
        [SerializeField]
        private float _maxValue;
        [SerializeField]
        private GameObject _prefab;
    }
}