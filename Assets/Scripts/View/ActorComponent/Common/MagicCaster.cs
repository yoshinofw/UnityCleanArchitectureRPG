using System.Collections.Generic;
using UnityEngine;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class MagicCaster : MonoBehaviour, IEventBusUser, IMagicHand
    {
        public EventBus EventBus { get; set; }
        [SerializeField]
        private Transform _castPosition;
        private GameObject _magic;

        public void ChangeMagic(GameObject prefab)
        {
            _magic = prefab;
        }

        private void Cast()
        {
            Projectile magic = Instantiate(_magic, _castPosition.position, transform.rotation).GetComponent<Projectile>();
            magic.CasterLayer = gameObject.layer;
            magic.Collided += HandleCollided;
        }

        private void HandleCollided(HashSet<GameObject> targets, Vector3 direction)
        {
            foreach (var target in targets)
            {
                EventBus.Post(new HitboxTriggered(gameObject, target, "Magic", direction));
            }
        }
    }
}