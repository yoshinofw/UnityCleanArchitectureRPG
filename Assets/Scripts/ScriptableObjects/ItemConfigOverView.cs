using System;
using System.Collections.Generic;
using UnityEngine;

namespace UCARPG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemConfigOverView", menuName = "UCARPG/ItemConfigOverView", order = 0)]
    public class ItemConfigOverView : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemConfig this[string id] { get => _itemConfigsById[id]; }
        private Dictionary<string, ItemConfig> _itemConfigsById;
        [SerializeField]
        private WeaponConfig[] _weaponConfigs;
        [SerializeField]
        private MagicConfig[] _magicConfigs;
        [SerializeField]
        private ConsumableConfig[] _consumableConfigs;

        public void OnAfterDeserialize()
        {
            _itemConfigsById = new Dictionary<string, ItemConfig>();
            foreach (var weaponConfig in _weaponConfigs)
            {
                _itemConfigsById[weaponConfig.Id] = weaponConfig;
            }
            foreach (var magicConfig in _magicConfigs)
            {
                _itemConfigsById[magicConfig.Id] = magicConfig;
            }
            foreach (var consumableConfig in _consumableConfigs)
            {
                _itemConfigsById[consumableConfig.Id] = consumableConfig;
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }

    public abstract class ItemConfig
    {
        public string Id { get => _id; }
        public Sprite Icon { get => _icon; }
        public string Type { get => _type; }
        public int Capacity { get => _capacity; }
        [SerializeField]
        private string _id;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private string _type;
        [SerializeField]
        private int _capacity;
        public abstract string GetDescription();
    }

    [Serializable]
    public class WeaponConfig : ItemConfig
    {
        public GameObject Prefab { get => _prefab; }
        public RuntimeAnimatorController RuntimeAnimatorController { get => _runtimeAnimatorController; }
        public float HealthDamage { get => _healthDamage; }
        public float PoiseDamage { get => _poiseDamage; }
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private RuntimeAnimatorController _runtimeAnimatorController;
        [SerializeField]
        private float _healthDamage;
        [SerializeField]
        private float _poiseDamage;

        public override string GetDescription()
        {
            string result = Id + Environment.NewLine;
            result += "Damage: " + HealthDamage;
            return result;
        }
    }

    [Serializable]
    public class MagicConfig : ItemConfig
    {
        public GameObject Prefab { get => _prefab; }
        public float ManaCost { get => _manaCost; }
        public float HealthDamage { get => _healthDamage; }
        public float PoiseDamage { get => _poiseDamage; }
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private float _manaCost;
        [SerializeField]
        private float _healthDamage;
        [SerializeField]
        private float _poiseDamage;

        public override string GetDescription()
        {
            string result = Id + Environment.NewLine;
            result += "Damage: " + HealthDamage + Environment.NewLine;
            result += "ManaCost: " + ManaCost;
            return result;
        }
    }

    [Serializable]
    public class ConsumableConfig : ItemConfig
    {
        public string ModifyStatType { get => _modifyStatType; }
        public float Modifier { get => _modifier; }
        [SerializeField]
        private string _modifyStatType;
        [SerializeField]
        private float _modifier;

        public override string GetDescription()
        {
            string result = Id + Environment.NewLine;
            result += "Effect: " + ModifyStatType + (_modifier >= 0 ? "+" : "-") + _modifier;
            return result;
        }
    }
}