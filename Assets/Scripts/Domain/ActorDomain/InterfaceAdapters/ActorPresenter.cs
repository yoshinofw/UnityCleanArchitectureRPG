using System.Collections.Generic;
using UnityEngine;

namespace UCARPG.Domain.ActorDomain.InterfaceAdapters
{
    public class ActorPresenter : MonoBehaviour
    {
        public string PlayerActorId { get; private set; }
        public string this[GameObject viewObject] { get => _idsByViewObject[viewObject]; }
        public GameObject this[string id] { get => _viewObjectsById[id]; }
        private Dictionary<GameObject, string> _idsByViewObject = new Dictionary<GameObject, string>();
        private Dictionary<string, GameObject> _viewObjectsById = new Dictionary<string, GameObject>();
        private Dictionary<string, IMotionPerformer> _motionPerformersById = new Dictionary<string, IMotionPerformer>();
        private Dictionary<string, IWeaponHand> _weaponHandsById = new Dictionary<string, IWeaponHand>();
        private Dictionary<string, IMagicHand> _magicHandsById = new Dictionary<string, IMagicHand>();
        private Dictionary<string, IParticleSystemPlayer> _particleSystemPlayersById = new Dictionary<string, IParticleSystemPlayer>();
        private (Vector3 Position, Quaternion Rotation, Vector3[] PatrolPath) _createArgs;
        [SerializeField]
        private Transform _actorParent;
        [SerializeField]
        private MonoBehaviour _eventBusProvider;
        private Queue<IPatrolableActor> _waitForSetChaseTarget = new Queue<IPatrolableActor>();

        public void OnActorSpawnerExecuted(Vector3 position, Quaternion rotation, Vector3[] patrolPath)
        {
            _createArgs = (position, rotation, patrolPath);
        }

        public void OnPlayerActorCreated(GameObject viewObject)
        {
            PlayerActorId = _idsByViewObject[viewObject];
            while (_waitForSetChaseTarget.Count > 0)
            {
                IPatrolableActor patrolableActor = _waitForSetChaseTarget.Dequeue();
                patrolableActor.ChaseTarget = viewObject.transform;
                patrolableActor.Enabled = true;
            }
        }

        public void OnActorCreated(string id, GameObject prefab, RuntimeAnimatorController runtimeAnimatorController, GameObject weapon)
        {
            GameObject viewObject = Instantiate(prefab, _createArgs.Position, _createArgs.Rotation, _actorParent);
            viewObject.GetComponent<IActorComponentEventBusInjector>().Inject(_eventBusProvider);
            viewObject.GetComponent<IRevivableActor>()?.SetRevivePosition(_createArgs.Position);
            if (viewObject.GetComponent<IPatrolableActor>() is IPatrolableActor patrolableActor)
            {
                patrolableActor.PatrolPath = _createArgs.PatrolPath;
                if (string.IsNullOrEmpty(PlayerActorId))
                {
                    _waitForSetChaseTarget.Enqueue(patrolableActor);
                }
                else
                {
                    patrolableActor.ChaseTarget = _viewObjectsById[PlayerActorId].transform;
                    patrolableActor.Enabled = true;
                }
            }
            _idsByViewObject.Add(viewObject, id);
            _viewObjectsById.Add(id, viewObject);
            _motionPerformersById.Add(id, viewObject.GetComponent<IMotionPerformer>());
            _weaponHandsById.Add(id, viewObject.GetComponent<IWeaponHand>());
            _magicHandsById.Add(id, viewObject.GetComponent<IMagicHand>());
            _particleSystemPlayersById.Add(id, viewObject.GetComponent<IParticleSystemPlayer>());
            OnActorWeaponChanged(id, runtimeAnimatorController, weapon);
        }

        public void OnActorDirectionChanged(string id, float x, float y)
        {
            _motionPerformersById[id].ChangeDirection(new Vector3(x, 0, y));
        }

        public void OnActorRunStateChanged(string id, bool isRun)
        {
            _motionPerformersById[id].ChangeRunState(isRun);
        }

        public void OnActorLocomotionStateReseted(string id)
        {
            _motionPerformersById[id].ResetLocomotionState();
        }

        public void OnActorActionPerformed(string id, string action)
        {
            _motionPerformersById[id].PerformAction(action);
        }

        public void OnActorWeaponChanged(string id, RuntimeAnimatorController runtimeAnimatorController, GameObject prefab)
        {
            _motionPerformersById[id].RuntimeAnimatorController = runtimeAnimatorController;
            _weaponHandsById[id].ChangeWeapon(prefab);
        }

        public void OnActorMagicChanged(string id, GameObject prefab)
        {
            _magicHandsById[id].ChangeMagic(prefab);
        }

        public void OnHitboxTriggered(string id, Vector3 Damagedirection)
        {
            _motionPerformersById[id].SetGetHitDirection(Damagedirection);
        }

        public void OnActorRemoved(string id)
        {
            _particleSystemPlayersById[id].Play("Die");
            Destroy(_viewObjectsById[id]);
            _idsByViewObject.Remove(_viewObjectsById[id]);
            _viewObjectsById.Remove(id);
            _motionPerformersById.Remove(id);
            _weaponHandsById.Remove(id);
            _magicHandsById.Remove(id);
        }

        public void PlayParticleSystem(string id, string name)
        {
            _particleSystemPlayersById[id].Play(name);
        }
    }
}