using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class WeaponHitter : MonoBehaviour, IEventBusUser, IWeaponHand
    {
        public EventBus EventBus { get; set; }
        [SerializeField]
        private Transform _holdWeaponPoint;
        [SerializeField]
        private Animator _animator;
        private HitboxSetter _hitboxSetter;

        public void ChangeWeapon(GameObject prefab)
        {
            for (int i = 0; i < _holdWeaponPoint.childCount; i++)
            {
                Destroy(_holdWeaponPoint.GetChild(i).gameObject);
            }
            _hitboxSetter = Instantiate(prefab, _holdWeaponPoint).GetComponent<HitboxSetter>();
        }

        private void Hit(float endProgress)
        {
            StartCoroutine(Detect(_animator, endProgress));
        }

        private IEnumerator Detect(Animator animator, float endProgress)
        {
            HashSet<Collider> colliderRecords = new HashSet<Collider>();
            Vector3[] lastHitboxFaceCenters = _hitboxSetter.GetFaceCenters();
            Vector3[] currentHitboxFaceCenters;
            Vector3 direction;
            int layerMask = LayerMask.GetMask("Hurtbox");
            GameObject target;
            yield return new WaitForFixedUpdate();
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= endProgress)
            {
                currentHitboxFaceCenters = _hitboxSetter.GetFaceCenters();
                for (int i = 0; i < 6; i++)
                {
                    direction = currentHitboxFaceCenters[i] - lastHitboxFaceCenters[i];
                    foreach (var hit in Physics.RaycastAll(lastHitboxFaceCenters[i], direction, direction.magnitude, layerMask))
                    {
                        target = hit.transform.parent.gameObject;
                        if (target.layer != gameObject.layer && colliderRecords.Add(hit.collider))
                        {
                            EventBus.Post(new HitboxTriggered(gameObject, target, "Weapon", direction));
                        }
                    }
                }
                lastHitboxFaceCenters = currentHitboxFaceCenters;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}