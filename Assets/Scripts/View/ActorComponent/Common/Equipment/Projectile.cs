using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class Projectile : MonoBehaviour
    {
        public event Action<HashSet<GameObject>, Vector3> Collided;
        public int CasterLayer { get; set; }
        [SerializeField]
        private HitboxSetter _hitboxSetter;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _lifeTime;
        [SerializeField]
        private ParticleSystem _flash;
        [SerializeField]
        private ParticleSystem _hit;

        private void Start()
        {
            StartCoroutine(Detect());
            _flash.transform.parent = null;
            Destroy(_flash.gameObject, 1);
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
            transform.position += _speed * Time.deltaTime * transform.forward;
        }

        private IEnumerator Detect()
        {
            HashSet<GameObject> targets = new HashSet<GameObject>();
            Vector3[] lastHitboxFaceCenters = _hitboxSetter.GetFaceCenters();
            Vector3[] currentHitboxFaceCenters;
            Vector3 direction;
            int layerMask = LayerMask.GetMask(new string[] { "Default", "Hurtbox" });
            int hurtboxLayer = LayerMask.NameToLayer("Hurtbox");
            GameObject target;
            bool isCollide = false;
            yield return new WaitForFixedUpdate();
            while (true)
            {
                currentHitboxFaceCenters = _hitboxSetter.GetFaceCenters();
                for (int i = 0; i < 6; i++)
                {
                    direction = currentHitboxFaceCenters[i] - lastHitboxFaceCenters[i];
                    foreach (var hit in Physics.RaycastAll(lastHitboxFaceCenters[i], direction, direction.magnitude, layerMask))
                    {
                        isCollide = true;
                        if (hit.transform.gameObject.layer != hurtboxLayer)
                        {
                            continue;
                        }
                        target = hit.transform.parent.gameObject;
                        if (target.layer != CasterLayer)
                        {
                            targets.Add(target);
                        }
                    }
                }
                if (isCollide)
                {
                    Collided?.Invoke(targets, transform.forward);
                    Destroy(gameObject);
                    _hit.transform.parent = null;
                    _hit.Play();
                    Destroy(_hit.gameObject, 1);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}